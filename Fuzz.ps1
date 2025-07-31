#Requires -Version 7.4

[CmdletBinding()]
param (
  [ValidateSet('Bshox.Fuzz.Meta', 'Bshox.Fuzz.Text')]
  [Parameter(Mandatory)]
  [string]$ProjectName,

  [ValidateSet('net8.0', 'net10.0')]
  [Parameter()]
  [string]$tfm = 'net10.0',

  [ValidateRange(1, 32)]
  [Parameter()]
  [int]$Processors = 1
)

Set-StrictMode -Version 3.0
$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

if (-Not $IsLinux) {
  throw 'OS not supported. Use Linux instead.'
}

dotnet tool restore

$project = Get-Item ".\tests\fuzz\$ProjectName\$ProjectName.csproj"
$inDir = Get-Item ".\tests\fuzz\$ProjectName\TestCases"

$timeout = 10000

$outputDir = 'bin'
$findingsDir = 'findings'

$projectDll = "$ProjectName.dll"
$target = Join-Path $outputDir $ProjectName

if (Test-Path $outputDir) {
  Remove-Item -Recurse -Force $outputDir
}

if (Test-Path $findingsDir) {
  Remove-Item -Recurse -Force $findingsDir
}

dotnet publish $project -c Debug -f $tfm -o $outputDir

$exclusions = @(
  'dnlib.dll',
  'SharpFuzz.dll',
  'SharpFuzz.Common.dll',
  $projectDll
)

$fuzzingTargets = Get-ChildItem $outputDir -Filter '*.dll' `
| Where-Object { $_.Name -notin $exclusions } `
| Where-Object { $_.Name -notlike 'System.*.dll' }

if (($fuzzingTargets | Measure-Object).Count -eq 0) {
  throw 'No fuzzing targets found'
}

foreach ($fuzzingTarget in $fuzzingTargets) {
  Write-Output "Instrumenting $fuzzingTarget"
  dotnet sharpfuzz $fuzzingTarget.FullName 'Bshox'

  if ($LastExitCode -ne 0) {
    throw "An error occurred while instrumenting $fuzzingTarget"
  }
}

$env:AFL_I_DONT_CARE_ABOUT_MISSING_CRASHES = 1 # Required for WSL
$env:AFL_SKIP_BIN_CHECK = 1 # https://github.com/Metalnem/sharpfuzz/issues/22
$env:AFL_NO_UI = $null
# $env:AFL_NO_UI = 1

$Jobs = @()

try {

  for ($i = 1; $i -lt $Processors; $i++) {

    $Jobs += Start-Job -ScriptBlock {
      param($inDir, $findingsDir, $timeout, $target, $Name)
      afl-fuzz -i $inDir -o $findingsDir -t $timeout -S $Name $target -m 50 -t 500
    } -ArgumentList $inDir, $findingsDir, $timeout, $target, "Sub$i" -Name "Fuzzer-Sub$i"
  }

  $arg = $null
  if ($Processors -gt 1) {
    $arg = '-M', 'Main'
  }

  afl-fuzz -i $inDir -o $findingsDir -t $timeout $arg $target -m 50 -t 500
}
finally {
  $Jobs | Stop-Job -Verbose
  $Jobs | Remove-Job

  # Sanitize filenames
  # : is not allowed in filenames on Windows and causes issues since WSL will replace it with  (0xF03A)
  Get-ChildItem $findingsDir -Recurse -Filter '*:*' | ForEach-Object { Rename-Item $_ -NewName ($_.Name -replace ':', '=') }
}
