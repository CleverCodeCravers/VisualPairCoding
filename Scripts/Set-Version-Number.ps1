Param(
    [string]$NewVersion
)

$versionNumber = $NewVersion -replace '^refs/tags/', ''

Set-Location $PSScriptRoot

Set-Location ..\Source\VisualPairCoding\VisualPairCoding.BL

$versionInformation = Get-Content VersionInformation.cs -Raw -Encoding UTF8
$versionInformation = $versionInformation.Replace('$$VERSION$$', $versionNumber)

$versionInformation | Set-Content VersionInformation.cs -Encoding UTF8
