using System.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;
using VisualPairCoding.BL.AutoUpdates;

namespace VisualPairCoding.Infrastructure;
public class AutoUpdater
{
    private readonly string _appName;
    private readonly string _appVersion;
    private readonly string _githubUrl;

    public AutoUpdater(
        string appName,
        string appVersion,
        string githubUrl)
    {
        _appName = appName;
        _appVersion = appVersion;
        _githubUrl = githubUrl;
    }

    public GithubAPIResponse[] GetReleaseList() 
    {
        using (var client = new HttpClient())
        {
            var webRequest = new HttpRequestMessage(HttpMethod.Get, _githubUrl);
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:103.0) Gecko/20100101 Firefox/103.0");

            var response = client.Send(webRequest);

            string result = response.Content.ReadAsStringAsync().Result.Trim();

            GithubAPIResponse[] releases = JsonConvert.DeserializeObject<GithubAPIResponse[]>(result);

            return releases;
        }
    }


    public void Update()
    {
        GithubAPIResponse[] releases = GetReleaseList();

        using (WebClient downloadClient = new WebClient())
        {
            downloadClient.DownloadFile(releases[0].Assets[0].Browser_download_url, releases[0].Assets[0].Name);
        }

        var cwd = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        string path = cwd + "\\" + "updater.ps1";

        var script =
            @"
Set-Location $PSScriptRoot
$ErrorActionPreference = ""Stop""

Start-Sleep -Seconds 5

$Successful = $false
While (-not ($Successful)) {
    try {
        Expand-Archive -Path ""$pwd\VisualPairCoding-win-x64.zip"" -DestinationPath $pwd -Force
        $Successful = $true
    } catch {
        Write-Host ""Waiting for application to quit...""
        Start-Sleep -Seconds 5
    }
}

Start-Sleep -Seconds 5

Start-Process ""VisualPairCoding.WinForms.exe""
Start-Sleep -Seconds 5
Remove-Item -Path ""$pwd\VisualPairCoding-win-x64.zip"" -Force
Start-Sleep -Seconds 5
Remove-Item -Path ""$pwd\updater.ps1"" -Force
";

        if (!File.Exists(path))
        {
            File.WriteAllText(path, script);
        }
        
        try
        {
            var startInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy ByPass -File \"{path}\"",
                UseShellExecute = false
            };
            
            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Boolean IsUpdateAvailable()
    {
        GithubAPIResponse[] releases = this.GetReleaseList();
        if (releases[0].Name != _appName + " v" + _appVersion)
        {
            return true;
        }

        return false;

    }
}
