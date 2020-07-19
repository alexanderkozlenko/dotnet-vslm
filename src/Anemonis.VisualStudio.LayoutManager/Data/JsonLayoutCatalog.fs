// © Alexander Kozlenko. Licensed under the MIT License.

namespace Anemonis.VisualStudio.LayoutManager.Data

open System.Text.Json.Serialization

[<Sealed>]
type public JsonLayoutCatalog() =
    class
        [<JsonPropertyName("packages")>]
        member val public Packages : JsonLayoutPackage[] = null
            with get, set
    end
