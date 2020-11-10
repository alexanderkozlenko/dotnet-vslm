// © Alexander Kozlenko. Licensed under the MIT License.

namespace Anemonis.Resources

open System
open System.Diagnostics
open System.Reflection
open System.Text

[<DebuggerStepThrough>]
[<Sealed>]
[<AbstractClass>]
type internal EmbeddedResourceManager() =
    class
        static member private s_assembly : Assembly = Assembly.GetExecutingAssembly()
        static member private s_assemblyName : string = Assembly.GetExecutingAssembly().GetName().Name

        static member public GetString(name : string) : string =
            use resourceStream = EmbeddedResourceManager.s_assembly.GetManifestResourceStream(EmbeddedResourceManager.s_assemblyName + "." + name)

            if resourceStream = null then
                raise (InvalidOperationException($"The resource \"{name}\" was not found"))

            let buffer = Array.create<byte>(int32(resourceStream.Length))(byte(0))

            resourceStream.Read(buffer, 0, buffer.Length)
            |> ignore

            Encoding.UTF8.GetString(buffer)
    end
