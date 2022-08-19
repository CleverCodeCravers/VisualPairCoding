Param(
    [string]$NewVersion
)

Set-Location $PSScriptRoot

Set-Location ..\Source\VisualPairCoding\VisualPairCoding.BL\AutoUpdates

$versionInformation = Get-Content VersionInformation.cs -Raw -Encoding UTF8
$versionInformation = $versionInformation.Replace("$$VERSION$$", $NewVersion)

$versionInformation | Set-Content VersionInfomration.cs -Encoding UTF8