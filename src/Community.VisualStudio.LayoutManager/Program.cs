using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using Community.VisualStudio.LayoutManager.Engine;
using Community.VisualStudio.LayoutManager.Resources;
using Microsoft.Extensions.Configuration;

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

                var (manifestPackages, manifestVersion) = CatalogLoader.LoadManifestPackages(layoutPath);
                var layoutPackages = CatalogLoader.LoadLayoutPackages(layoutPath);

                var obsoletePackages = layoutPackages.Except(manifestPackages, CatalogPackageComparer.Default)
                    .OrderBy(x => x.ID)
                    .ThenBy(x => x.Version)
                    .ThenBy(x => x.Chip)
                    .ThenBy(x => x.Language)
                    .ToArray();

                switch (command)
                {
                    case "reveal":
                        {
                            Console.WriteLine(Strings.GetString("command.reveal.info_message"), manifestVersion);
                            Console.WriteLine();

                            var totalSize = 0L;

                            foreach (var package in obsoletePackages)
                            {
                                var packageLocation = Path.Combine(layoutPath, CatalogPackageFormatter.GetPackageDirectoryName(package));
                                var packageSize = DirectoryContentInfo.GetSize(packageLocation);

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
                            Console.WriteLine(Strings.GetString("command.clean.info_message"), manifestVersion);
                            Console.WriteLine();

                            var totalSize = 0L;

                            foreach (var package in obsoletePackages)
                            {
                                var packageLocation = Path.Combine(layoutPath, CatalogPackageFormatter.GetPackageDirectoryName(package));
                                var packageSize = DirectoryContentInfo.GetSize(packageLocation);

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
    }
}