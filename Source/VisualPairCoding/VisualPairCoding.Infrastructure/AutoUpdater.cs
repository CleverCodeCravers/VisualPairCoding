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

        var cwd = Assembly.GetExecutingAssembly().Location;
        string path = cwd + "\\" + "updater.ps1";

        var script =
            "Set-Location $PSScriptRoot" + Environment.NewLine +
            "Expand-Archive -Path \"$pwd\\VisualPairCoding-win-x64.zip\" -DestinationPath $pwd -Force" + Environment.NewLine +
            "Start-Process \"VisualPairCoding.WinForms.exe\"" + Environment.NewLine +
            "Remove-Item -Path \"$pwd\\VisualPairCoding-win-x64.zip\" -Force" + Environment.NewLine +
            "Remove-Item -Path \"$pwd\\updater.ps1\" -Force";

        File.WriteAllText("updater.ps1", script);
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
