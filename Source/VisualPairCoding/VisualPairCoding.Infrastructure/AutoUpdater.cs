using Microsoft.Win32;
using Newtonsoft.Json;
using System.Net;
using VisualPairCoding.BL;

namespace VisualPairCoding.Infrastructure;
public class AutoUpdater
{
    private readonly string _appVersion;
    private WebClient downloadClient = new WebClient();

    public AutoUpdater(
        string appVersion)
    {
        _appVersion = appVersion;
    }


    public GithubAPIResponse[] GetLatestVersion() {

        var releaseURL = "https://api.github.com/repos/stho32/VisualPairCoding/releases";

        var client = new HttpClient();

        var webRequest = new HttpRequestMessage(HttpMethod.Get, releaseURL);
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:103.0) Gecko/20100101 Firefox/103.0");

        var response = client.Send(webRequest);

        string result = response.Content.ReadAsStringAsync().Result.Trim();
        GithubAPIResponse[] releases = JsonConvert.DeserializeObject<GithubAPIResponse[]>(result);

        return releases;
    }


    public  Boolean RegisterVersionInRegistery()
    {
        RegistryKey key;

        var getLocalVersion = Registry.CurrentUser.OpenSubKey("VisualPairCoding");

        if (getLocalVersion == null)
        {
            key = Registry.CurrentUser.CreateSubKey("VisualPairCoding");
            key.SetValue("version", _appVersion);
            key.Close();
        }

        return true;

    }

    public void Update()
    {
        GithubAPIResponse[] releases = this.GetLatestVersion();
        bool checkRegistery = this.RegisterVersionInRegistery();

        if (checkRegistery == false)
        {
            throw new Exception("Could not add the App to the registery!");
        }

        downloadClient.DownloadFile(releases[0].Assets[0].Browser_download_url, releases[0].Assets[0].Name);
        Registry.CurrentUser.OpenSubKey("VisualPairCoding", true).SetValue("version", releases[0].Name);
    }

    public Boolean IsUpdateAvailable()
    {
        GithubAPIResponse[] releases = this.GetLatestVersion();
        if (releases[0].Name != Registry.CurrentUser.OpenSubKey("VisualPairCoding").GetValue("version").ToString())
        {
            return true;
        }

        return false;

    }
}
