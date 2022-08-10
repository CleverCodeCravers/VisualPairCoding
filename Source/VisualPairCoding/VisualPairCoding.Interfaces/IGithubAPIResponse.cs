namespace VisualPairCoding.Interfaces;
public interface IGithubAPIResponse
{
    string Url { get; }
    string Assets_url { get; }
    string Upload_url { get; }
    string Html_url { get; }
    int Id { get; }
    IGithubAuthor Author { get; }
    string Node_id { get; }
    string Tag_name { get; }
    string Target_commitish { get; }
    string Name { get; }
    bool Draft { get; }
    bool Prerelease { get; }
    DateTime Created_at { get; }
    DateTime Published_at { get; }
    IGithubReleaseAsset[] Assets { get; }
    string Tarball_url { get; }
    string Zipball_url { get; }
    string Body { get; }

}


public interface IGithubAuthor
{
    string Login { get; }
    int Id { get; }
    string Node_id { get; }
    string Avatar_url { get; }
    string Gravatar_id { get; }
    string Url { get; }
    string Html_url { get; }
    string Followers_url { get; }
    string Following_url { get; }
    string Gists_url { get; }
    string Starred_url { get; }
    string Subscriptions_url { get; }
    string Organizations_url { get; }
    string Repos_url { get; }
    string Events_url { get; }
    string Received_events_url { get; }
    string Type { get; }
    bool Site_admin { get; }

}


public interface IGithubReleaseAsset
{
    string Url { get; }
    int Id { get; }
    string Node_id { get; }
    string Name { get; }
    string Label { get; }
    IGithubReleaseUploader Uploader { get; }
    string Content_type { get; }
    string State { get; }
    int Size { get; }
    int Download_count { get; }
    DateTime Created_at { get; }
    DateTime Updated_at { get; }
    string Browser_download_url { get; }

}

public interface IGithubReleaseUploader
{
    string Login { get; }
    int Id { get; }
    string Node_id { get; }
    string Avatar_url { get; }
    string Gravatar_id { get; }
    string Url { get; }
    string Html_url { get; }
    string Followers_url { get; }
    string Following_url { get; }
    string Gists_url { get; }
    string Starred_url { get; }
    string Subscriptions_url { get; }
    string Organizations_url { get; }
    string Repos_url { get; }
    string Events_url { get; }
    string Received_events_url { get; }
    string Type { get; }
    bool Site_admin { get; }

}