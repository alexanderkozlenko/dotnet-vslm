## Visual Studio 2017 Installation Layout Manager

Provides additional abilities for managing installation layout of Visual Studio 2017

### Run instructions:

```
dotnet vslm.dll --layout-path <value> --command <value>
```

### Available commands:

Command | Purpose
--- | ---
`list-obsolete` | List obsolete packages which are not included in the catalog
`remove-obsolete` | Remove obsolete packages which are not included in the catalog

[![Latest release](https://img.shields.io/github/release/alexanderkozlenko/vs-layout-manager.svg?style=flat-square)](https://github.com/alexanderkozlenko/vs-layout-manager/releases)