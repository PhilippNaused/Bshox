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
$env:TUNIT_DISABLE_HTML_REPORTER = $true

if ($Generator) {
  $Path = 'tests/Generator.Benchmark'
}
else {
  $Path = 'tests/Benchmark'
}

dotnet test --disable-logo --project 'tests/Benchmark.Tests' --configuration Release
dotnet run --project $Path --configuration Release --framework $tfm

# cspell:ignore maziac
# Fix code fences in the generated markdown files to use 'asm' instead of 'assembly' for syntax highlighting.
# Otherwise, the 'maziac.asm-code-lens' extension won't recognize it.
$Path = Join-Path $PSScriptRoot 'docs\benchmarks\results\*-asm.md'
$Files = Get-ChildItem -Path $Path
foreach ($File in $Files) {
  $Content = Get-Content -Path $File.FullName
  $Content = $Content -replace '```assembly', '```asm'
  $Content = $Content -join "`n" # Force unix-style line endings on Windows
  $Content = $Content.TrimEnd() # Remove trailing newlines
  $Content | Out-File -FilePath $File.FullName -Encoding utf8NoBOM -NoNewline
}
