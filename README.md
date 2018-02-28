## Visual Studio Layout Manager

A tool for listing and removing obsolete packages from the Visual Studio installation layout (version 2017 and later). 

```
dotnet vslm.dll --layout <value> [--command <value>]
```

Parameter | Mandatory | Default Value | Purpose
--- | :---: | --- | ---
`--layout` | Yes | | The layout directory
`--command` | No | `reveal` | The command to execute

Command | Purpose
--- | ---
`reveal` | List obsolete packages from the layout
`clean` | Remove obsolete packages from the layout

### Specifics

- Each directory in the layout that matches the package directory pattern is considered as a package directory.
- Each local package which is not listed in the `Catalog.json` is considered as obsolete package.

### Examples

Output example for the `reveal` command:

```
Visual Studio Layout Manager version 1.0.0

Found Anaconda2.Exe.x64 5.0.0 x64 (522,426,032 bytes)
Found Anaconda2.Exe.x86 5.0.0 (421,720,568 bytes)
Found Anaconda3.Exe.x64 5.0.0 x64 (534,742,736 bytes)
Found Anaconda3.Exe.x86 5.0.0 (436,033,392 bytes)

Summary: found 4 obsolete package(s) (1,914,922,728 bytes)
```

[![Latest release](https://img.shields.io/github/release/alexanderkozlenko/vs-layout-manager.svg?style=flat-square)](https://github.com/alexanderkozlenko/vs-layout-manager/releases)