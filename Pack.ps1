#Requires -Version 7.4

[CmdletBinding()]
param (
)

Set-StrictMode -Version 3.0
$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

if (Test-Path '.\publish') {
  Remove-Item '.\publish\*' -Recurse -Force
}

dotnet build --configuration Release '.\src\Packages.slnf'
dotnet pack --configuration Release '.\src\Packages.slnf' --output '.\publish' --no-build

$GlobalNuGetCache = dotnet nuget locals -l global-packages | ForEach-Object {
  if ($_ -match 'global-packages: *(.+)') {
    return Get-Item $matches[1]
  }
}

# Remove all dev builds from the global NuGet cache
Join-Path $GlobalNuGetCache 'bshox' | Get-ChildItem -Filter '*-dev*' -ErrorAction Ignore | Remove-Item -Recurse
