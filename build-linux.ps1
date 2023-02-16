function Write-MessageInFrame {
  [CmdletBinding()]
  param (
    [Parameter(Mandatory = $true)]
    [string]$Message
  )

  $borderChar = "+"
  $paddingChar = " "
  $termWidth = $Host.UI.RawUI.WindowSize.Width
  $maxLength = ($Message | Measure-Object -Maximum -Property Length).Maximum
  $borderLength = [math]::Max($maxLength + 4, $termWidth)
  $border = $borderChar * $borderLength


  Write-Host ""
  Write-Host $border
  Write-Host "$borderChar$paddingChar$Message$paddingChar$borderChar"
  Write-Host $border
  Write-Host ""
}

function Get-NonLibraryCsprojFiles {
  [CmdletBinding()]
  param (
    [Parameter(Mandatory = $true)]
    [string]$Path
  )

  # Find all csproj files in the directory
  $csprojFiles = Get-ChildItem -Path $Path -Recurse -Include *.csproj

  # Loop through each csproj file and check its output type
  foreach ($csproj in $csprojFiles) {
    $xml = [xml](Get-Content $csproj.FullName)
    $outputType = $xml.Project.PropertyGroup.OutputType

    if ($outputType -in "WinExe", "Exe") {
      $csproj.FullName
    }
  }
}


$outputDirectory = "$PSScriptRoot\output"
$slnFile = Get-ChildItem -Recurse -Filter *.sln -ErrorAction SilentlyContinue

if (Test-Path $outputDirectory) {
  Remove-Item $outputDirectory -Recurse
}

if (-not([bool]$slnFile)) {
  Write-Host "No .sln file was found in the specified folder or its subfolders."
}

Push-Location -Path $slnFile.DirectoryName

Write-MessageInFrame "$($slnFile.FullName) building..."
dotnet build --configuration Release
if ($LASTEXITCODE -gt 0) {
  Write-Error "Build failed with exit code $($LASTEXITCODE)"
  exit $LASTEXITCODE
}


Write-MessageInFrame "testing"

dotnet test 
if ($LASTEXITCODE -gt 0) {
  Write-Error "Build failed with exit code $($LASTEXITCODE)"
  exit $LASTEXITCODE
}

Write-MessageInFrame "publish"

#dotnet publish $($slnFile.FullName) --configuration Release --runtime linux-x64 --self-contained true --output $outputDirectory /p:PublishSingleFile=true /p:IncludeSymbols=false /p:DebugType=None
$projects_to_publish = Get-NonLibraryCsprojFiles ./ 

if ([bool]$projects_to_publish) {
  $projects_to_publish | Foreach-Object {
    $currentProject = $_

    $relativePath = "./" + $currentProject.Substring($slnFile.DirectoryName.Length + 1)
    Write-Host "  -> $relativePath" -ForegroundColor Cyan

    dotnet publish $relativePath --configuration Release --output $outputDirectory /p:IncludeSymbols=false /p:DebugType=None 
  }
}

if ($LASTEXITCODE -gt 0) {
  Write-Error "Publishing failed with exit code $($LASTEXITCODE)"
  exit $LASTEXITCODE
}

Pop-Location
