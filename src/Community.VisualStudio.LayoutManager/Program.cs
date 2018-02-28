using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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

            Console.WriteLine(Strings.GetString("program.assembly_info"), assembly.GetCustomAttribute<AssemblyProductAttribute>().Product, assembly.GetName().Version.ToString(3));
            Console.WriteLine(assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright);
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
                var provider = new LayoutPackagesProvider();

                var catalogPackages = provider.AcquireCatalogPackages(File.ReadAllText(Path.Combine(layoutPath, "Catalog.json"), Encoding.UTF8));
                var localPackages = provider.AcquireLocalPackages(Directory.GetDirectories(layoutPath));

                var obsoletePackages = localPackages.Except(catalogPackages)
                    .OrderBy(x => x.Id)
                    .ThenBy(x => x.Version)
                    .ThenBy(x => x.Chip)
                    .ThenBy(x => x.Language)
                    .ToArray();

                switch (command)
                {
                    case "reveal":
                        {
                            var totalSize = 0L;

                            foreach (var package in obsoletePackages)
                            {
                                var packageLocation = Path.Combine(layoutPath, package.GetDirectoryName());
                                var packageSize = new DirectoryInfo(packageLocation).GetSize();

                                totalSize += packageSize;

                                Console.WriteLine(Strings.GetString("command.reveal.package_info"), package, packageSize);
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
                            var totalSize = 0L;

                            foreach (var package in obsoletePackages)
                            {
                                var packageLocation = Path.Combine(layoutPath, package.GetDirectoryName());
                                var packageSize = new DirectoryInfo(packageLocation).GetSize();

                                totalSize += packageSize;

                                Directory.Delete(packageLocation, true);
                                Console.WriteLine(Strings.GetString("command.clean.package_info"), package, packageSize);
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

                var foregroundColor = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(Strings.GetString("program.error_message"), ex.Message);
                Console.ForegroundColor = foregroundColor;
                Console.WriteLine();
                Console.WriteLine(Strings.GetString("program.usage_message"), Path.GetFileName(assembly.Location));
            }
        }
    }
}