## Visual Studio 2017 Setup Layout Manager

Provides an ability to display and remove obsolete packages from the Visual Studio 2017 installation layout. Each package which is not listed in the current catalog file `Catalog.json` is considered as absolete.

### Run instruction:

```
dotnet vslm.dll --layout <value> [--command <value>]
```

### Supported parameters:

Parameter | Mandatory | Default Value | Purpose
--- | :---: | --- | ---
`--layout` | Yes | | Installation layout path
`--command` | No | `reveal` | Command to execute

### Supported commands:

Command | Purpose
--- | ---
`reveal` | List obsolete packages
`clean` | Remove obsolete packages

### Output sample (`reveal` command):

```
VS2017 Setup Layout Manager 1.0.0

Listing obsolete packages from layout "15.5.4+27130.2024":

- Anaconda2.Exe.x64 5.0.0 x64 / 522,426,032 bytes
- Anaconda2.Exe.x86 5.0.0 / 421,720,568 bytes
- Anaconda3.Exe.x64 5.0.0 x64 / 534,742,736 bytes
- Anaconda3.Exe.x86 5.0.0 / 436,033,392 bytes
- AndroidBuildSupport 1.0.40 / 317,633,203 bytes

Summary: 5 obsolete package(s) / 2,232,555,931 bytes
```

[![Latest release](https://img.shields.io/github/release/alexanderkozlenko/vs-layout-manager.svg?style=flat-square)](https://github.com/alexanderkozlenko/vs-layout-manager/releases)