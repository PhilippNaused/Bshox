#Requires -Version 7.4

[CmdletBinding()]
param (
  # Opens the html report (in your default browser).
  [Parameter()]
  [switch]$Open
)

Set-StrictMode -Version 3.0
$ErrorActionPreference = 'Stop'
$PSNativeCommandUseErrorActionPreference = $true

# restore ReportGenerator
dotnet tool restore

$ReportDir = 'coverage-report'
$CoverageDir = 'coverage'

# remove old coverage data and reports
Remove-Item $CoverageDir -Recurse -ErrorAction Ignore
Remove-Item $ReportDir -Recurse -ErrorAction Ignore

New-Item -Type Directory $CoverageDir -ErrorAction Ignore

# Use solution filter to only test projects
dotnet test --no-ansi --solution .\tests\UnitTests.slnf --coverage --coverage-output-format 'xml' --results-directory $CoverageDir -p:PublishAot=false

# create the report
dotnet ReportGenerator -Reports:"$CoverageDir/*" -TargetDir:"$ReportDir" -ReportTypes:'HtmlInline;MarkdownSummaryGithub'
Copy-Item (Join-Path $ReportDir 'SummaryGithub.md') -Destination (Join-Path $PSScriptRoot 'Coverage.md') -Force

if ($Open) {
  Invoke-Item $ReportDir\index.html
}
