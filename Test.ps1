#Requires -Version 7.4

[CmdletBinding()]
param (
  [Parameter()]
  [ValidateSet('Debug', 'Release')]
  [string]$Configuration = 'Debug'
)

Set-StrictMode -Version 3.0
$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

dotnet build --configuration $Configuration -p:PublishAot=false

# Run regular tests using MTP
dotnet test --no-ansi --configuration $Configuration --no-build

# Publish AOT tests
Remove-Item -Path "$PSScriptRoot/Temp" -Recurse -Force -ErrorAction Ignore
dotnet build --configuration $Configuration '.\tests\Bshox.Tests\' -t:PublishAll --no-dependencies -p:PublishDir="$PSScriptRoot/Temp/Bshox.Tests"

# Run AOT tests using MTP
dotnet test --no-ansi --test-modules "Temp/**/*.exe"
