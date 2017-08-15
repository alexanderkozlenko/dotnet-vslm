[string]$workspace = "$PSScriptRoot\..\.."
[string]$configuration = "Release"

& dotnet build "$workspace\src\" --configuration $configuration

If ($LASTEXITCODE -ne 0) {
    Throw "Build command failed with code $LASTEXITCODE"
}