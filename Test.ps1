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
Remove-Item -Path "$PSScriptRoot\temp\*" -Recurse -Force -ErrorAction Ignore
dotnet build --configuration $Configuration "$PSScriptRoot\tests\Bshox.Tests" -t:PublishAll --no-dependencies -p:PublishDir="$PSScriptRoot\temp\Bshox.Tests"

# Run AOT tests using MTP
if ($IsWindows) {
  dotnet test --no-ansi --root-directory $PSScriptRoot --test-modules "temp\**\Bshox.Tests.exe"
}
else {
  dotnet test --no-ansi --root-directory $PSScriptRoot --test-modules "temp\**\Bshox.Tests"
}
