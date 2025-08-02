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
