using VisualPairCoding.Interfaces;

namespace VisualPairCoding.BL.AutoUpdates;

public class GithubReleaseUploader : IGithubReleaseUploader
{
    public string Login { get; }

    public int Id { get; }

    public string Node_id { get; }

    public string Avatar_url { get; }

    public string Gravatar_id { get; }

    public string Url { get; }

    public string Html_url { get; }

    public string Followers_url { get; }

    public string Following_url { get; }

    public string Gists_url { get; }

    public string Starred_url { get; }

    public string Subscriptions_url { get; }

    public string Organizations_url { get; }

    public string Repos_url { get; }

    public string Events_url { get; }

    public string Received_events_url { get; }

    public string Type { get; }

    public bool Site_admin { get; }

    public GithubReleaseUploader(string login, int id, string node_id, string avatar_url, string gravatar_id, string url, string html_url, string followers_url, string following_url, string gists_url, string starred_url, string subscriptions_url, string organizations_url, string repos_url, string events_url, string received_events_url, string type, bool site_admin)
    {
        Login = login;
        Id = id;
        Node_id = node_id;
        Avatar_url = avatar_url;
        Gravatar_id = gravatar_id;
        Url = url;
        Html_url = html_url;
        Followers_url = followers_url;
        Following_url = following_url;
        Gists_url = gists_url;
        Starred_url = starred_url;
        Subscriptions_url = subscriptions_url;
        Organizations_url = organizations_url;
        Repos_url = repos_url;
        Events_url = events_url;
        Received_events_url = received_events_url;
        Type = type;
        Site_admin = site_admin;
    }
}