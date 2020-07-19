// © Alexander Kozlenko. Licensed under the MIT License.

namespace Anemonis.VisualStudio.LayoutManager.Data

open System.Text.Json.Serialization

[<Sealed>]
type public JsonLayoutPackage() =
    class
        [<JsonPropertyName("id")>]
        member val public ID : string = null
            with get, set

        [<JsonPropertyName("version")>]
        member val public Version : string = null
            with get, set

        [<JsonPropertyName("chip")>]
        member val public Architecture : string = null
            with get, set

        [<JsonPropertyName("language")>]
        member val public Language : string = null
            with get, set
    end
