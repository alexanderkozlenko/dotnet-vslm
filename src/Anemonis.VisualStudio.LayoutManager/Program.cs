// © Alexander Kozlenko. Licensed under the MIT License.

using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Reflection;
using System.Text;

using Anemonis.VisualStudio.LayoutManager.Data;
using Anemonis.VisualStudio.LayoutManager.Engine;
using Anemonis.VisualStudio.LayoutManager.Resources;

#pragma warning disable CA1031

namespace Anemonis.VisualStudio.LayoutManager
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var assembly = typeof(Program).Assembly;

            Console.WriteLine(Strings.GetString("program.assembly_info"), assembly.GetCustomAttribute<AssemblyProductAttribute>().Product, assembly.GetName().Version.ToString(3));
            Console.WriteLine(assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright?.Replace("\u00A9", "(c)", StringComparison.Ordinal));
            Console.WriteLine();

            var pathArgument = new Argument("path")
            {
                ArgumentType = typeof(string),
                Description = Strings.GetString("argument.path.description")
            };

            pathArgument.SetDefaultValue(Environment.CurrentDirectory);

            var commandList = new Command("list")
            {
                Description = Strings.GetString("command.list.description")
            };

            commandList.AddArgument(pathArgument);
            commandList.Handler = CommandHandler.Create((Action<string>)((path) => ExecuteAction(ApplicationCommand.List, path)));

            var commandClean = new Command("clean")
            {
                Description = Strings.GetString("command.clean.description")
            };

            commandClean.AddArgument(pathArgument);
            commandClean.Handler = CommandHandler.Create((Action<string>)((path) => ExecuteAction(ApplicationCommand.Clean, path)));

            var commandRoot = new RootCommand();

            commandRoot.AddCommand(commandList);
            commandRoot.AddCommand(commandClean);

            commandRoot.Handler = CommandHandler.Create(() => ExecuteAction(ApplicationCommand.List, (string)pathArgument.GetDefaultValue()));

            commandRoot.Invoke(args);
        }

        private static void ExecuteAction(ApplicationCommand action, string layoutPath)
        {
            try
            {
                var provider = new LayoutPackagesProvider();

                var catalogPackages = provider.GetCatalogPackages(File.ReadAllText(Path.Combine(layoutPath, "Catalog.json"), Encoding.UTF8));
                var localPackages = provider.GetLocalPackages(Directory.GetDirectories(layoutPath));
                var obsoletePackages = provider.GetObsoletePackages(catalogPackages, localPackages);

                switch (action)
                {
                    case ApplicationCommand.List:
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
                    case ApplicationCommand.Clean:
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
                }
            }
            catch (Exception ex)
            {
                Environment.ExitCode = 1;

                var foregroundColor = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine(Strings.GetString("program.error_message"), ex.Message);
                Console.ForegroundColor = foregroundColor;
            }
        }
    }
}
