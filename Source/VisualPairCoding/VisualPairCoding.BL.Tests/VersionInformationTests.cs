namespace VisualPairCoding.BL.Tests;

public class VersionInformationTests
{
    [Fact]
    public void Version_ReturnsNonEmptyString()
    {
        var version = VersionInformation.Version;

        Assert.NotNull(version);
        Assert.NotEmpty(version);
    }

    [Fact]
    public void Version_IsNotBlank()
    {
        var version = VersionInformation.Version;

        // In dev it's "$$VERSION$$", in CI it's replaced by the git ref (tag or branch)
        Assert.False(string.IsNullOrWhiteSpace(version), "Version should not be blank");
    }
}
