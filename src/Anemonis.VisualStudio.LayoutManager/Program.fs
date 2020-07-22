// © Alexander Kozlenko. Licensed under the MIT License.

namespace Anemonis.VisualStudio.LayoutManager

open System
open System.CommandLine
open System.CommandLine.Invocation
open System.IO
open System.Reflection
open System.Text

open Anemonis.VisualStudio.LayoutManager.Data
open Anemonis.VisualStudio.LayoutManager.Engine
open Anemonis.VisualStudio.LayoutManager.Resources

module public Program =
    let private ExecuteAction(action : ApplicationCommand, layoutPath : string) : unit =
        try
            let provider = new LayoutPackagesProvider()
            let catalogPackages = provider.GetCatalogPackages(File.ReadAllText(Path.Combine(layoutPath, "Catalog.json"), Encoding.UTF8))
            let localPackages = provider.GetLocalPackages(Directory.GetDirectories(layoutPath))
            let obsoletePackages = provider.GetObsoletePackages(catalogPackages, localPackages)

            match action with
            | ApplicationCommand.List ->
                let mutable totalSize = 0L

                for package in obsoletePackages do
                    let packageLocation = Path.Combine(layoutPath, package.ToString())
                    let packageSize = (new DirectoryInfo(packageLocation)).GetSize()

                    totalSize <- totalSize + packageSize

                    Console.WriteLine(Strings.GetString("command.list.package_info"), package, packageSize)

                if obsoletePackages.Count > 0 then
                    Console.WriteLine()

                Console.WriteLine(Strings.GetString("command.list.summary_message"), obsoletePackages.Count, totalSize)
            | ApplicationCommand.Clean ->
                let mutable totalSize = 0L

                for package in obsoletePackages do
                    let packageLocation = Path.Combine(layoutPath, package.ToString())
                    let packageSize = (new DirectoryInfo(packageLocation)).GetSize()

                    totalSize <- totalSize + packageSize

                    Directory.Delete(packageLocation, true)
                    Console.WriteLine(Strings.GetString("command.clean.package_info"), package, packageSize)

                if obsoletePackages.Count > 0 then
                    Console.WriteLine()

                Console.WriteLine(Strings.GetString("command.clean.summary_message"), obsoletePackages.Count, totalSize)
            | _ -> ignore()
        with
            | e ->
                Environment.ExitCode <- 1
                
                let foregroundColor = Console.ForegroundColor
                
                Console.ForegroundColor <- ConsoleColor.Red
                Console.Error.WriteLine(Strings.GetString("program.error_message"), e.Message)
                Console.ForegroundColor <- foregroundColor

    [<EntryPoint>]
    let public Main(args: string[]) : int32 =
        let entryAssembly = Assembly.GetEntryAssembly()
        let attributeProduct = entryAssembly.GetCustomAttribute<AssemblyProductAttribute>()
        let attributeCopyright = entryAssembly.GetCustomAttribute<AssemblyCopyrightAttribute>()

        Console.WriteLine(Strings.GetString("program.assembly_info"), attributeProduct.Product, entryAssembly.GetName().Version.ToString(3))
        Console.WriteLine(attributeCopyright.Copyright.Replace("\u00A9", "(c)", StringComparison.Ordinal))
        Console.WriteLine()

        let pathArgument = new Argument("path")

        pathArgument.ArgumentType <- typeof<string>
        pathArgument.Description <- Strings.GetString("argument.path.description")
        pathArgument.SetDefaultValue(Environment.CurrentDirectory)

        let commandList = new Command("list")

        commandList.Description <- Strings.GetString("command.list.description")
        commandList.AddArgument(pathArgument)
        commandList.Handler <- CommandHandler.Create(fun path -> ExecuteAction(ApplicationCommand.List, path))

        let commandClean = new Command("clean")

        commandClean.Description <- Strings.GetString("command.clean.description")
        commandClean.AddArgument(pathArgument)
        commandClean.Handler <- CommandHandler.Create(fun path -> ExecuteAction(ApplicationCommand.Clean, path))

        let commandRoot = new RootCommand()

        commandRoot.AddCommand(commandList)
        commandRoot.AddCommand(commandClean)
        commandRoot.Handler <- CommandHandler.Create(fun () -> ExecuteAction(ApplicationCommand.List, pathArgument.GetDefaultValue() :?> string))
        commandRoot.Invoke(args) |> ignore

        0
