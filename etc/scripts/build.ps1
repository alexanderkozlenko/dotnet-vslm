[string]$workspace = "$PSScriptRoot\..\.."
[string]$configuration = "Release"

& dotnet restore "$workspace\src\"

If ($LASTEXITCODE -ne 0) {
    Throw "Restore command failed with code $LASTEXITCODE"
}

& dotnet build "$workspace\src\" --configuration $configuration

If ($LASTEXITCODE -ne 0) {
    Throw "Build command failed with code $LASTEXITCODE"
}