## Visual Studio 2017 Installation Layout Manager

Provides an ability to display and remove obsolete packages from the Visual Studio 2017 installation layout. Each package which is not listed in the current catalog file (Catalog.json) is considered as absolete.

### Run instruction:

```
dotnet vslm.dll --layout-path <value> --command <value>
```

### Available commands:

Command | Purpose
--- | ---
`list-obsolete` | List obsolete packages
`remove-obsolete` | Remove obsolete packages

[![Latest release](https://img.shields.io/github/release/alexanderkozlenko/vs-layout-manager.svg?style=flat-square)](https://github.com/alexanderkozlenko/vs-layout-manager/releases)