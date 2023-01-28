using VisualPairCoding.Interfaces;

namespace VisualPairCoding.BL.AutoUpdates;
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