// © Alexander Kozlenko. Licensed under the MIT License.

namespace Anemonis.VisualStudio.LayoutManager.Data

open System
open System.Text

[<AllowNullLiteral>]
[<Sealed>]
type public LayoutPackage(id : string, version : string, architecture : string, language : string) =
    class
        do
            if id = null then
                raise (ArgumentNullException(nameof(id)))
            if version = null then
                raise (ArgumentNullException(nameof(version)))

        member private _._id : string = id
        member private _._version : string = version
        member private _._architecture : string = architecture
        member private _._language : string = language

        interface IEquatable<LayoutPackage> with
            member this.Equals(other : LayoutPackage) : bool =
                match other with
                | null -> false
                | _ ->
                    (String.Compare(this._id, other._id, StringComparison.OrdinalIgnoreCase) = 0) &&
                    (String.Compare(this._version, other._version, StringComparison.OrdinalIgnoreCase) = 0) &&
                    (String.Compare(this._architecture, other._architecture, StringComparison.OrdinalIgnoreCase) = 0) &&
                    (String.Compare(this._language, other._language, StringComparison.OrdinalIgnoreCase) = 0)

        override this.Equals(obj : Object) : bool =
            match obj with
            | :? LayoutPackage as other ->
                (this :> IEquatable<LayoutPackage>).Equals(other)
            | _ -> false

        override this.GetHashCode() : int32 =
            let valueID =
                this._id.ToUpperInvariant()

            let valueVersion =
                this._version.ToUpperInvariant()

            let valueArchitecture =
                match this._architecture with
                | null -> null
                | _ -> this._architecture.ToUpperInvariant()

            let valueLanguage =
                match this._language with
                | null -> null
                | _ -> this._language.ToUpperInvariant()

            HashCode.Combine(
                valueID,
                valueVersion,
                valueArchitecture,
                valueLanguage)

        override this.ToString() : string =
            let builder = new StringBuilder(this._id)

            builder
                .Append(',')
                .Append("version")
                .Append('=')
                .Append(this._version)
                |> ignore

            if this._architecture <> null then
                builder
                    .Append(',')
                    .Append("chip")
                    .Append('=')
                    .Append(this._architecture)
                    |> ignore
            
            if this._language <> null then
                builder
                    .Append(',')
                    .Append("language")
                    .Append('=')
                    .Append(this._language)
                    |> ignore

            builder.ToString()

        member public this.ID
            with get() : string =
                this._id

        member public this.Version
            with get() : string =
                this._version

        member public this.Architecture
            with get() : string =
                this._architecture

        member public this.Language
            with get() : string =
                this._language
    end
