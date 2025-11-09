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

cspell lint $PSScriptRoot

# Run regular tests using MTP
dotnet test --disable-logo --configuration $Configuration

# Publish AOT tests
$ProjectDir = Join-Path $PSScriptRoot 'tests\Bshox.Tests'
$PublishDir = Join-Path $ProjectDir 'bin' $Configuration 'publish-aot'
Remove-Item -Path $PublishDir -Recurse -Force -ErrorAction Ignore
dotnet build --configuration $Configuration $ProjectDir -t:PublishAll --no-dependencies -p:PublishDir=$PublishDir -p:DebugSymbols=false

# Run AOT tests using MTP
$Glob = $IsWindows ? '*/Bshox.Tests.exe' : '*/Bshox.Tests'
dotnet test --disable-logo --root-directory $PublishDir --test-modules $Glob
