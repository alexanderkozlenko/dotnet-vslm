// © Alexander Kozlenko. Licensed under the MIT License.

using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

using Anemonis.VisualStudio.LayoutManager.Engine;
using Anemonis.VisualStudio.LayoutManager.Resources;

namespace Anemonis.VisualStudio.LayoutManager
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var assembly = typeof(Program).Assembly;

            Console.WriteLine(Strings.GetString("program.assembly_info"), assembly.GetCustomAttribute<AssemblyProductAttribute>().Product, assembly.GetName().Version.ToString(3));
            Console.WriteLine(assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright?.Replace("\u00A9", "(c)"));
            Console.WriteLine();

            try
            {
                var command = args.Length > 0 ? args[0].ToLowerInvariant() : "--list";
                var layoutPath = args.Length > 1 ? args[1] : Environment.CurrentDirectory;

                var provider = new LayoutPackagesProvider();

                var catalogPackages = provider.GetCatalogPackages(File.ReadAllText(Path.Combine(layoutPath, "Catalog.json"), Encoding.UTF8));
                var localPackages = provider.GetLocalPackages(Directory.GetDirectories(layoutPath));
                var obsoletePackages = provider.GetObsoletePackages(catalogPackages, localPackages);

                switch (command)
                {
                    case "--list":
                        {
                            var totalSize = 0L;

                            foreach (var package in obsoletePackages)
                            {
                                var packageLocation = Path.Combine(layoutPath, package.ToString());
                                var packageSize = new DirectoryInfo(packageLocation).GetSize();

                                totalSize += packageSize;

                                Console.WriteLine(Strings.GetString("command.list.package_info"), package, packageSize);
                            }
                            if (obsoletePackages.Count > 0)
                            {
                                Console.WriteLine();
                            }

                            Console.WriteLine(Strings.GetString("command.list.summary_message"), obsoletePackages.Count, totalSize);
                        }
                        break;
                    case "--clean":
                        {
                            var totalSize = 0L;

                            foreach (var package in obsoletePackages)
                            {
                                var packageLocation = Path.Combine(layoutPath, package.ToString());
                                var packageSize = new DirectoryInfo(packageLocation).GetSize();

                                totalSize += packageSize;

                                Directory.Delete(packageLocation, true);
                                Console.WriteLine(Strings.GetString("command.clean.package_info"), package, packageSize);
                            }
                            if (obsoletePackages.Count > 0)
                            {
                                Console.WriteLine();
                            }

                            Console.WriteLine(Strings.GetString("command.clean.summary_message"), obsoletePackages.Count, totalSize);
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
                Console.WriteLine(Strings.GetString("program.usage_message"));
                Console.WriteLine();
                Console.WriteLine(Strings.GetString("program.usage_commands"));
                Console.WriteLine();
                Console.WriteLine(Strings.GetString("program.usage_command_list"));
                Console.WriteLine(Strings.GetString("program.usage_command_clean"));
            }
        }
    }
}