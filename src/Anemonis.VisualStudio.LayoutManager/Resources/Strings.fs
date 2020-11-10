// © Alexander Kozlenko. Licensed under the MIT License.

namespace Anemonis.VisualStudio.LayoutManager.Resources

open System.Globalization
open System.Resources

[<Sealed>]
[<AbstractClass>]
type internal Strings() =
    class
        static member private s_resourceManager : ResourceManager =
            new ResourceManager(typeof<Strings>.Namespace + "." + typeof<Strings>.Name, typeof<Strings>.Assembly)

        static member public GetString(name: string) : string =
            Strings.s_resourceManager.GetString(name, CultureInfo.CurrentCulture)
    end
