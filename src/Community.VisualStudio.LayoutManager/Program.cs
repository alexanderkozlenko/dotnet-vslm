using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Community.VisualStudio.LayoutManager.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Community.VisualStudio.LayoutManager
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var assembly = typeof(Program).Assembly;

            Console.WriteLine($"{assembly.GetCustomAttribute<AssemblyProductAttribute>().Product} {assembly.GetName().Version.ToString(3)}");
            Console.WriteLine();

            var configurationBuilder = new ConfigurationBuilder().AddCommandLine(args);

            try
            {
                var configuration = configurationBuilder.Build();
                var layoutPath = configuration["layout"];

                if (layoutPath == null)
                {
                    throw new InvalidOperationException("\"layout\" parameter is not specified");
                }

                var command = configuration["command"] ?? "reveal";
                var catalogInfo = default(CatalogInfo);

                using (var stream = new FileStream(Path.Combine(layoutPath, "Catalog.json"), FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        catalogInfo = JsonConvert.DeserializeObject<CatalogInfo>(reader.ReadToEnd());
                    }
                }

                var layoutPackages = new List<CatalogPackageInfo>();
                var packageNameRegex = new Regex("^(?<id>[^,]+),version=(?<version>[^,]+)(?:,chip=(?<chip>[^,]+))?(?:,language=(?<language>[^,]+))?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                foreach (var directoryPath in Directory.GetDirectories(layoutPath))
                {
                    var match = packageNameRegex.Match(Path.GetFileName(directoryPath));

                    if (match.Success)
                    {
                        var package = new CatalogPackageInfo
                        {
                            ID = match.Groups["id"].Value,
                            Version = match.Groups["version"].Value,
                            Chip = match.Groups["chip"].Success ? match.Groups["chip"].Value : null,
                            Language = match.Groups["language"].Success ? match.Groups["language"].Value : null
                        };

                        layoutPackages.Add(package);
                    }
                }

                var obsoletePackages = layoutPackages.Except(catalogInfo.Packages, CatalogPackageComparer.Default)
                    .OrderBy(x => x.ID)
                    .ThenBy(x => x.Version)
                    .ToArray();

                switch (command)
                {
                    case "reveal":
                        {
                            Console.WriteLine($"Listing obsolete packages in layout \"{catalogInfo.Product.SemanticVersion}\"...");
                            Console.WriteLine();

                            var totalSize = 0L;

                            foreach (var package in obsoletePackages)
                            {
                                var packageName = GetPackageName(package);
                                var packageLocation = Path.Combine(layoutPath, packageName);
                                var packageSize = GetDirectorySize(packageLocation);

                                totalSize += packageSize;

                                Console.WriteLine($"- {packageName} ({packageSize:#,0} bytes)");
                            }
                            if (obsoletePackages.Length > 0)
                            {
                                Console.WriteLine();
                            }

                            Console.WriteLine($"Total: {obsoletePackages.Length} obsolete package(s) in {totalSize:#,0} bytes");
                        }
                        break;
                    case "clean":
                        {
                            Console.WriteLine($"Removing obsolete packages from layout \"{catalogInfo.Product.SemanticVersion}\"...");
                            Console.WriteLine();

                            var totalSize = 0L;

                            foreach (var package in obsoletePackages)
                            {
                                var packageName = GetPackageName(package);
                                var packageLocation = Path.Combine(layoutPath, packageName);
                                var packageSize = GetDirectorySize(packageLocation);

                                totalSize += packageSize;

                                Console.WriteLine($"- {packageName} ({packageSize:#,0} bytes)");

                                if (Directory.Exists(packageLocation))
                                {
                                    Directory.Delete(packageLocation, true);
                                }
                            }
                            if (obsoletePackages.Length > 0)
                            {
                                Console.WriteLine();
                            }

                            Console.WriteLine($"Total: {obsoletePackages.Length} obsolete package(s) in {totalSize:#,0} bytes");
                        }
                        break;
                    default:
                        {
                            throw new InvalidOperationException("The specified command is invalid");
                        }
                }
            }
            catch (Exception ex)
            {
                Environment.ExitCode = 1;

                Console.WriteLine($"ERROR: {ex.Message}");
                Console.WriteLine();

                var assemblyFile = Path.GetFileName(assembly.Location);

                Console.WriteLine($"Usage: dotnet {assemblyFile} --layout <value> [--command <value>]");
            }
        }

        private static string GetPackageName(CatalogPackageInfo package)
        {
            var builder = new StringBuilder($"{package.ID},version={package.Version}");

            if (package.Chip != null)
            {
                builder.Append($",chip={package.Chip}");
            }
            if (package.Language != null)
            {
                builder.Append($",language={package.Language}");
            }

            return builder.ToString();
        }

        private static long GetDirectorySize(string directoryPath)
        {
            void GetDirectorySize(DirectoryInfo directory, ref long totalSize)
            {
                foreach (var subdirectory in directory.GetDirectories())
                {
                    GetDirectorySize(subdirectory, ref totalSize);
                }
                foreach (var file in directory.GetFiles())
                {
                    totalSize += file.Length;
                }
            }

            var result = 0L;

            GetDirectorySize(new DirectoryInfo(directoryPath), ref result);

            return result;
        }
    }
}