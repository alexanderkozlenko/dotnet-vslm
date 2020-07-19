# Visual Studio 2019 Layout Manager

A [.NET Core Global Tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) for working with obsolete Visual Studio 2019 installation layout packages. 

| [![gitter.im](https://img.shields.io/gitter/room/nwjs/nw.js.svg?style=flat-square)](https://gitter.im/anemonis/dotnet-vslm) | Release | Current |
|---|---|---|
| Artifacts | [![nuget.org](https://img.shields.io/nuget/vpre/dotnet-vslm.svg?style=flat-square)](https://www.nuget.org/packages/dotnet-vslm) | [![feedz.io](https://img.shields.io/endpoint.svg?url=https%3A%2F%2Ff.feedz.io%2Falexander-kozlenko%2Fanemonis%2Fshield%2Fdotnet-vslm%2Flatest&label=feedz&logo=_&color=lightgrey&style=flat-square)](https://f.feedz.io/alexander-kozlenko/anemonis/packages/dotnet-vslm/latest/download) |
| Build Status | | [![dev.azure.com](https://img.shields.io/azure-devops/build/alexanderkozlenko/github-pipelines/7?label=master&style=flat-square)](https://dev.azure.com/alexanderkozlenko/github-pipelines/_build?definitionId=7&_a=summary) |

## Project Details

- Each directory in the layout that matches the package directory pattern is considered as a package directory.
- Each local package which is not listed in the `Catalog.json` file is considered as obsolete package.

```
dotnet vslm [command [path]]
```

Parameters:

| Parameter | Default Value | Purpose |
| --- | --- | --- |
| `command` | `list` | Application command to execute |
| `path` | `.\` | Path to the installation layout |

Commands:

| Command | Purpose |
| --- | --- |
| `list` | List obsolete packages from the layout |
| `clean` | Remove obsolete packages from the layout |

## Usage Examples

```
dotnet tool install -g dotnet-vslm
```
```
dotnet vslm list \\server\vs2019\
```
```
Visual Studio 2019 Layout Manager version 1.0.0

- "Package1,version=1.0.0,chip=x64" (426,032 bytes)
- "Package1,version=1.0.0,chip=x86" (720,568 bytes)

Summary: found 2 obsolete package(s) (1,146,600 bytes)
```

## Quicklinks

- [Contributing Guidelines](./CONTRIBUTING.md)
- [Code of Conduct](./CODE_OF_CONDUCT.md)
