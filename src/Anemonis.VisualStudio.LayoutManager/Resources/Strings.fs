// © Alexander Kozlenko. Licensed under the MIT License.

namespace Anemonis.VisualStudio.LayoutManager.Resources

open System.Globalization
open System.Resources

[<Sealed>]
[<AbstractClass>]
type internal Strings() =
    class
        static member private _resourceManager : ResourceManager =
            new ResourceManager(typeof<Strings>.Namespace + "." + typeof<Strings>.Name, typeof<Strings>.Assembly)

        static member public GetString(name: string) : string =
            Strings._resourceManager.GetString(name, CultureInfo.CurrentCulture)
    end
