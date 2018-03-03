param (
    [parameter(mandatory)][String]$configuration
)

function execute {
    param(
        [parameter(mandatory)][ScriptBlock]$command
    )

    & $command;
    
    if ($lastexitcode -eq $null) {
        throw "`"$command`" command failed";
    }
    if ($lastexitcode -ne 0) {
        throw "`"$command`" command failed with code $lastexitcode";
    }
}

[String]$sources = "$PSScriptRoot/src/";

execute { dotnet clean "$sources" -c $configuration };
execute { dotnet build "$sources" -c $configuration };

foreach ($project in (get-childitem -path "$sources" -file -include "*.Tests.csproj" -recurse)) {
    execute { dotnet test "$project" -c $configuration --no-restore --no-build };
}
foreach ($project in (get-childitem -path "$sources" -file -include "*.csproj" -exclude @("*.Tests.csproj", "*.Benchmarks.csproj") -recurse)) {
    execute { dotnet publish "$project" -c $configuration --no-restore };
}