#Requires -Version 7.4

[CmdletBinding()]
param (
  [ValidateSet('net8.0', 'net9.0', 'net10.0', 'net48')]
  [Parameter()]
  [string]$tfm = 'net10.0',

  [Parameter()]
  [switch]$Generator
)

Set-StrictMode -Version 3.0
$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

if ($Generator) {
  $Path = '.\tests\Generator.Benchmark'
}
else {
  $Path = '.\tests\Benchmark'
}

dotnet test --disable-logo --project '.\tests\Benchmark.Tests\' --configuration Release
dotnet run --project $Path --configuration Release --framework $tfm
