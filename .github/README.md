# Visual Studio 2019 Layout Manager

A .NET Core [Global Tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) for working with obsolete Visual Studio 2019 installation layout packages. 

[![NuGet](https://img.shields.io/nuget/v/dotnet-vslm.svg?style=flat-square)](https://www.nuget.org/packages/dotnet-vslm)
[![MyGet](https://img.shields.io/myget/alexanderkozlenko/vpre/dotnet-vslm.svg?label=myget&style=flat-square)](https://www.myget.org/feed/alexanderkozlenko/package/nuget/dotnet-vslm)

[![SonarCloud](https://img.shields.io/sonar/violations/dotnet-vslm?format=long&label=sonar&server=https%3A%2F%2Fsonarcloud.io&style=flat-square)](https://sonarcloud.io/dashboard?id=dotnet-vslm)

[![Gitter](https://img.shields.io/gitter/room/nwjs/nw.js.svg?style=flat-square)](https://gitter.im/anemonis/dotnet-vslm)

## Project Details

```
dotnet vslm [command] [layout] 
```

| Parameter | Default Value | Purpose |
| --- | --- | --- |
| `command` | `--list` | The command to execute |
| `layout` | `.\` | The layout directory |

| Command | Purpose |
| --- | --- |
| `--list` | Display obsolete packages from the layout |
| `--clean` | Remove obsolete packages from the layout |

- Each directory in the layout that matches the package directory pattern is considered as a package directory.
- Each local package which is not listed in the `Catalog.json` file is considered as obsolete package.

## Usage Examples

```
dotnet tool install --global dotnet-vslm
```
```
dotnet vslm --list \\server\vs2019\
```
```
Visual Studio 2019 Layout Manager version 1.0.0

Working with layout "\\server\vs2019\"

- "Package1,version=1.0.0,chip=x64" (426,032 bytes)
- "Package1,version=1.0.0,chip=x86" (720,568 bytes)

Summary: found 2 obsolete package(s) (1,146,600 bytes)
```

## Quicklinks

- [Contributing Guidelines](./CONTRIBUTING.md)
- [Code of Conduct](./CODE_OF_CONDUCT.md)
