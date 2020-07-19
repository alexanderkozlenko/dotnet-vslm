// © Alexander Kozlenko. Licensed under the MIT License.

namespace System.IO

open System.Runtime.CompilerServices

[<Extension>]
type public DirectoryInfoExtensions() =
    [<Extension>]
    static member public GetSize(directoryInfo: DirectoryInfo) : int64 =
        directoryInfo.GetFiles("*", SearchOption.AllDirectories)
        |> Seq.sumBy (fun x -> x.Length)
