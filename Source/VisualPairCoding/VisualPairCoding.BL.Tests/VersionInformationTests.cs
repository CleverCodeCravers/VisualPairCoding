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
    public void Version_ContainsPlaceholderOrValidVersion()
    {
        var version = VersionInformation.Version;

        // In dev/test it's the placeholder, in CI it's replaced with the actual version
        Assert.True(
            version == "$$VERSION$$" || System.Text.RegularExpressions.Regex.IsMatch(version, @"^\d+\.\d+"),
            $"Version should be placeholder or semver-like, got: {version}");
    }
}
