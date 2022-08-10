
using VisualPairCoding.Interfaces;

namespace VisualPairCoding.BL;
public class GithubAPIResponse : IGithubAPIResponse
{

    public string Url { get; }

    public string Assets_url { get; }

    public string Upload_url { get; }

    public string Html_url { get; }

    public int Id { get; }

    public GithubAuthor Author { get; }

    public string Node_id { get; }

    public string Tag_name { get; }

    public string Target_commitish { get; }

    public string Name { get; }

    public bool Draft { get; }

    public bool Prerelease { get; }

    public DateTime Created_at { get; }

    public DateTime Published_at { get; }

    public GithubReleaseAsset[] Assets { get; }

    public string Tarball_url { get; }

    public string Zipball_url { get; }

    public string Body { get; }

    IGithubAuthor IGithubAPIResponse.Author { get; }

    IGithubReleaseAsset[] IGithubAPIResponse.Assets { get; }

    public GithubAPIResponse(string url, string assets_url, string upload_url, string html_url, int id, GithubAuthor author, string node_id, string tag_name, string target_commitish, string name, bool draft, bool prerelease, DateTime created_at, DateTime published_at, GithubReleaseAsset[] assets, string tarball_url, string zipball_url, string body)
    {
        Url = url;
        Assets_url = assets_url;
        Upload_url = upload_url;
        Html_url = html_url;
        Id = id;
        Author = author;
        Node_id = node_id;
        Tag_name = tag_name;
        Target_commitish = target_commitish;
        Name = name;
        Draft = draft;
        Prerelease = prerelease;
        Created_at = created_at;
        Published_at = published_at;
        Assets = assets;
        Tarball_url = tarball_url;
        Zipball_url = zipball_url;
        Body = body;
    }
}

public class GithubAuthor : IGithubAuthor
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

public GithubAuthor(string login, int id, string node_id, string avatar_url, string gravatar_id, string url, string html_url, string followers_url, string following_url, string gists_url, string starred_url, string subscriptions_url, string organizations_url, string repos_url, string events_url, string received_events_url, string type, bool site_admin)
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

public class GithubReleaseAsset : IGithubReleaseAsset
{
    public string Url { get; }

    public int Id { get; }

    public string Node_id { get; }

    public string Name { get; }

    public string Label { get; }

    public GithubReleaseUploader Uploader { get; }

    public string Content_type { get; }

    public string State { get; }

    public int Size { get; }

    public int Download_count { get; }

    public DateTime Created_at { get; }

    public DateTime Updated_at { get; }

    IGithubReleaseUploader IGithubReleaseAsset.Uploader { get; }

    public string Browser_download_url { get; }

    public GithubReleaseAsset(string url, int id, string node_id, string name, string label, GithubReleaseUploader uploader, string content_type, string state, int size, int download_count, DateTime created_at, DateTime updated_at, string browser_download_url)
    {
        Url = url;
        Id = id;
        Node_id = node_id;
        Name = name;
        Label = label;
        Uploader = uploader;
        Content_type = content_type;
        State = state;
        Size = size;
        Download_count = download_count;
        Created_at = created_at;
        Updated_at = updated_at;
        Browser_download_url = browser_download_url;
    }
}
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
