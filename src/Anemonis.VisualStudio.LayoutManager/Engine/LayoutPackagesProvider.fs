// © Alexander Kozlenko. Licensed under the MIT License.

namespace Anemonis.VisualStudio.LayoutManager.Engine

open System
open System.Collections.Generic
open System.IO
open System.Text.Json
open System.Text.RegularExpressions

open Anemonis.VisualStudio.LayoutManager.Data

[<Sealed>]
type public LayoutPackagesProvider() =
    class
        static member private _packageNameRegex : Regex =
            new Regex("^(?<i>[^,]+),version=(?<v>[^,]+)(?:,chip=(?<c>[^,]+))?(?:,language=(?<l>[^,]+))?$", RegexOptions.IgnoreCase ||| RegexOptions.Compiled)

        member public this.GetCatalogPackages(json : string) : IReadOnlyCollection<LayoutPackage> =
            if json = null then
                raise (ArgumentNullException("json"))

            let catalog = JsonSerializer.Deserialize<JsonLayoutCatalog>(json)

            catalog.Packages
            |> Seq.map (fun x -> new LayoutPackage(x.ID, x.Version, x.Architecture, x.Language))
            |> HashSet<LayoutPackage>
            :> IReadOnlyCollection<LayoutPackage>

        member public this.GetLocalPackages(directories : IReadOnlyList<string>) : IReadOnlyCollection<LayoutPackage> =
            if directories = null then
                raise (ArgumentNullException("directories"))

            let getValue (g : Group) : string = (if g.Success then g.Value else null)

            directories
            |> Seq.map Path.GetFileName
            |> Seq.map LayoutPackagesProvider._packageNameRegex.Match
            |> Seq.where (fun x -> x.Success)
            |> Seq.map (fun x ->
                new LayoutPackage(
                    x.Groups.["i"].Value,
                    x.Groups.["v"].Value,
                    getValue(x.Groups.["c"]),
                    getValue(x.Groups.["l"])))
            |> HashSet<LayoutPackage>
            :> IReadOnlyCollection<LayoutPackage>

        member public this.GetObsoletePackages(catalogPackages : IReadOnlyCollection<LayoutPackage>, localPackages : IReadOnlyCollection<LayoutPackage>) : IReadOnlyCollection<LayoutPackage> =
            if catalogPackages = null then
                raise (ArgumentNullException("catalogPackages"))
            if localPackages = null then
                raise (ArgumentNullException("localPackages"))

            localPackages
            |> Seq.except catalogPackages
            |> Seq.sortBy (fun x -> x.ID, x.Version, x.Architecture, x.Language)
            |> HashSet<LayoutPackage>
            :> IReadOnlyCollection<LayoutPackage>
    end
