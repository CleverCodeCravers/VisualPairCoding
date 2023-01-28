using VisualPairCoding.Interfaces;

namespace VisualPairCoding.BL.AutoUpdates;

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