#Requires -Version 7.4

[CmdletBinding()]
param (
  [Parameter()]
  [ValidateSet('Debug', 'Release', 'Both')]
  [string]$Configuration = 'Both'
)

Set-StrictMode -Version 3.0
$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

if ($Configuration -eq 'Both') {
  dotnet build --target 'BuildAll'
}
else {
  dotnet build --configuration $Configuration
}
