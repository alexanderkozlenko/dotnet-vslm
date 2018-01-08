using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Community.VisualStudio.LayoutManager.Data;
using Community.VisualStudio.LayoutManager.Resources;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Community.VisualStudio.LayoutManager
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var assembly = typeof(Program).Assembly;

            Console.WriteLine(assembly.GetCustomAttribute<AssemblyProductAttribute>().Product + " " + assembly.GetName().Version.ToString(3));
            Console.WriteLine();

            var configurationBuilder = new ConfigurationBuilder().AddCommandLine(args);

            try
            {
                var configuration = configurationBuilder.Build();
                var layoutPath = configuration["layout"];

                if (layoutPath == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Strings.GetString("program.undefined_parameter"), "layout"));
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
                var packageNameRegex = new Regex("^(?<i>[^,]+),version=(?<v>[^,]+)(?:,chip=(?<c>[^,]+))?(?:,language=(?<l>[^,]+))?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                foreach (var directoryPath in Directory.GetDirectories(layoutPath))
                {
                    var match = packageNameRegex.Match(Path.GetFileName(directoryPath));

                    if (match.Success)
                    {
                        var package = new CatalogPackageInfo
                        {
                            ID = match.Groups["i"].Value,
                            Version = match.Groups["v"].Value,
                            Chip = match.Groups["c"].Success ? match.Groups["c"].Value : null,
                            Language = match.Groups["l"].Success ? match.Groups["l"].Value : null
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
                            Console.WriteLine(Strings.GetString("command.reveal.info_message"), catalogInfo.Product.SemanticVersion);
                            Console.WriteLine();

                            var totalSize = 0L;

                            foreach (var package in obsoletePackages)
                            {
                                var packageLocation = Path.Combine(layoutPath, CatalogPackageFormatter.GetPackageDirectoryName(package));
                                var packageSize = GetDirectorySize(packageLocation);

                                totalSize += packageSize;

                                Console.WriteLine(Strings.GetString("command.reveal.package_info"), CatalogPackageFormatter.GetPackageName(package), packageSize);
                            }
                            if (obsoletePackages.Length > 0)
                            {
                                Console.WriteLine();
                            }

                            Console.WriteLine(Strings.GetString("command.reveal.summary_message"), obsoletePackages.Length, totalSize);
                        }
                        break;
                    case "clean":
                        {
                            Console.WriteLine(Strings.GetString("command.clean.info_message"), catalogInfo.Product.SemanticVersion);
                            Console.WriteLine();

                            var totalSize = 0L;

                            foreach (var package in obsoletePackages)
                            {
                                var packageLocation = Path.Combine(layoutPath, CatalogPackageFormatter.GetPackageDirectoryName(package));
                                var packageSize = GetDirectorySize(packageLocation);

                                totalSize += packageSize;

                                Console.WriteLine(Strings.GetString("command.clean.package_info"), CatalogPackageFormatter.GetPackageName(package), packageSize);

                                Directory.Delete(packageLocation, true);
                            }
                            if (obsoletePackages.Length > 0)
                            {
                                Console.WriteLine();
                            }

                            Console.WriteLine(Strings.GetString("command.clean.summary_message"), obsoletePackages.Length, totalSize);
                        }
                        break;
                    default:
                        {
                            throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, Strings.GetString("program.invalid_parameter"), "command"));
                        }
                }
            }
            catch (Exception ex)
            {
                Environment.ExitCode = 1;

                Console.WriteLine(Strings.GetString("program.error_message"), ex.Message);
                Console.WriteLine();
                Console.WriteLine(Strings.GetString("program.usage_message"), Path.GetFileName(assembly.Location));
            }
        }

        private static long GetDirectorySize(string directoryPath)
        {
            var result = 0L;

            foreach (var file in new DirectoryInfo(directoryPath).GetFiles("*", SearchOption.AllDirectories))
            {
                result += file.Length;
            }

            return result;
        }
    }
}