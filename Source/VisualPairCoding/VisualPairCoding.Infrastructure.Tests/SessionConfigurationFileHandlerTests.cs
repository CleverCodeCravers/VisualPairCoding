namespace VisualPairCoding.Infrastructure.Tests;

public class SessionConfigurationFileHandlerTests : IDisposable
{
    private readonly string _tempDir;
    private readonly SessionConfigurationFileHandler _handler;

    public SessionConfigurationFileHandlerTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), "VPCTests_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_tempDir);
        _handler = new SessionConfigurationFileHandler();
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempDir))
            Directory.Delete(_tempDir, true);
    }

    [Fact]
    public void GetFilenameProposal_SingleParticipant_ReturnsNameWithExtension()
    {
        var result = _handler.GetFilenameProposal(new[] { "Alice" });

        Assert.Equal("Alice.vpcsession", result);
    }

    [Fact]
    public void GetFilenameProposal_MultipleParticipants_JoinsWithUnderscore()
    {
        var result = _handler.GetFilenameProposal(new[] { "Alice", "Bob", "Charlie" });

        Assert.Equal("Alice_Bob_Charlie.vpcsession", result);
    }

    [Fact]
    public void GetFilenameProposal_EmptyArray_ReturnsJustExtension()
    {
        var result = _handler.GetFilenameProposal(Array.Empty<string>());

        Assert.Equal(".vpcsession", result);
    }

    [Fact]
    public void Save_CreatesFileOnDisk()
    {
        var filePath = Path.Combine(_tempDir, "test.vpcsession");
        var config = new SessionConfiguration(new[] { "Alice", "Bob" }, 7);

        _handler.Save(filePath, config);

        Assert.True(File.Exists(filePath));
    }

    [Fact]
    public void Save_WritesValidJson()
    {
        var filePath = Path.Combine(_tempDir, "test.vpcsession");
        var config = new SessionConfiguration(new[] { "Alice", "Bob" }, 7);

        _handler.Save(filePath, config);

        var json = File.ReadAllText(filePath);
        Assert.Contains("Alice", json);
        Assert.Contains("Bob", json);
    }

    [Fact]
    public void Load_ReadsConfigCorrectly()
    {
        var filePath = Path.Combine(_tempDir, "test.vpcsession");
        var original = new SessionConfiguration(new[] { "Alice", "Bob" }, 7);
        _handler.Save(filePath, original);

        var loaded = _handler.Load(filePath);

        Assert.Equal(original.Participants, loaded.Participants);
        Assert.Equal(original.SessionLength, loaded.SessionLength);
    }

    [Fact]
    public void SaveAndLoad_RoundTrip_PreservesAllData()
    {
        var filePath = Path.Combine(_tempDir, "roundtrip.vpcsession");
        var original = new SessionConfiguration(
            new[] { "Alice", "Bob", "Charlie", "Dave" }, 15);

        _handler.Save(filePath, original);
        var loaded = _handler.Load(filePath);

        Assert.Equal(original.Participants, loaded.Participants);
        Assert.Equal(original.SessionLength, loaded.SessionLength);
    }

    [Fact]
    public void SaveAndLoad_RoundTrip_WithSingleParticipant()
    {
        var filePath = Path.Combine(_tempDir, "single.vpcsession");
        var original = new SessionConfiguration(new[] { "Solo" }, 1);

        _handler.Save(filePath, original);
        var loaded = _handler.Load(filePath);

        Assert.Single(loaded.Participants);
        Assert.Equal("Solo", loaded.Participants[0]);
        Assert.Equal(1, loaded.SessionLength);
    }

    [Fact]
    public void SaveAndLoad_RoundTrip_WithTenParticipants()
    {
        var filePath = Path.Combine(_tempDir, "ten.vpcsession");
        var participants = Enumerable.Range(1, 10).Select(i => $"Person{i}").ToArray();
        var original = new SessionConfiguration(participants, 5);

        _handler.Save(filePath, original);
        var loaded = _handler.Load(filePath);

        Assert.Equal(10, loaded.Participants.Length);
        Assert.Equal(participants, loaded.Participants);
    }

    [Fact]
    public void Load_NonexistentFile_ThrowsFileNotFoundException()
    {
        var filePath = Path.Combine(_tempDir, "nonexistent.vpcsession");

        Assert.Throws<FileNotFoundException>(() => _handler.Load(filePath));
    }

    [Fact]
    public void Load_InvalidJson_ThrowsJsonException()
    {
        var filePath = Path.Combine(_tempDir, "invalid.vpcsession");
        File.WriteAllText(filePath, "not valid json {{{");

        Assert.Throws<System.Text.Json.JsonException>(() => _handler.Load(filePath));
    }

    [Fact]
    public void Save_OverwritesExistingFile()
    {
        var filePath = Path.Combine(_tempDir, "overwrite.vpcsession");
        var first = new SessionConfiguration(new[] { "Alice" }, 5);
        var second = new SessionConfiguration(new[] { "Bob", "Charlie" }, 10);

        _handler.Save(filePath, first);
        _handler.Save(filePath, second);
        var loaded = _handler.Load(filePath);

        Assert.Equal(new[] { "Bob", "Charlie" }, loaded.Participants);
        Assert.Equal(10, loaded.SessionLength);
    }
}
