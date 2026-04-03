namespace VisualPairCoding.Infrastructure.Tests;

public class SessionConfigurationFolderHandlerTests : IDisposable
{
    private readonly string _tempDir;
    private readonly SessionConfigurationFileHandler _fileHandler;
    private readonly SessionConfigurationFolderHandler _folderHandler;

    public SessionConfigurationFolderHandlerTests()
    {
        _tempDir = Path.Combine(Path.GetTempPath(), "VPCTests_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(_tempDir);
        _fileHandler = new SessionConfigurationFileHandler();
        _folderHandler = new SessionConfigurationFolderHandler(_tempDir, _fileHandler);
    }

    public void Dispose()
    {
        if (Directory.Exists(_tempDir))
            Directory.Delete(_tempDir, true);
    }

    [Fact]
    public void GetRecentSessionNames_EmptyFolder_ReturnsEmptyArray()
    {
        var result = _folderHandler.GetRecentSessionNames();

        Assert.Empty(result);
    }

    [Fact]
    public void SaveAsRecentSession_CreatesFileInFolder()
    {
        var config = new SessionConfiguration(new[] { "Alice" }, 7);

        _folderHandler.SaveAsRecentSession(config);

        Assert.True(File.Exists(Path.Combine(_tempDir, "Alice.vpcsession")));
    }

    [Fact]
    public void SaveAsRecentSession_AppearsInRecentSessionNames()
    {
        var config = new SessionConfiguration(new[] { "Alice" }, 7);

        _folderHandler.SaveAsRecentSession(config);
        var names = _folderHandler.GetRecentSessionNames();

        Assert.Contains("Alice", names);
    }

    [Fact]
    public void SaveAsRecentSession_MultipleParticipants_UsesUnderscoreJoinedName()
    {
        var config = new SessionConfiguration(new[] { "Alice", "Bob" }, 5);

        _folderHandler.SaveAsRecentSession(config);
        var names = _folderHandler.GetRecentSessionNames();

        Assert.Contains("Alice_Bob", names);
    }

    [Fact]
    public void LoadRecentSession_ReturnsCorrectConfiguration()
    {
        var config = new SessionConfiguration(new[] { "Alice", "Bob" }, 10);
        _folderHandler.SaveAsRecentSession(config);

        var loaded = _folderHandler.LoadRecentSession("Alice_Bob");

        Assert.Equal(new[] { "Alice", "Bob" }, loaded.Participants);
        Assert.Equal(10, loaded.SessionLength);
    }

    [Fact]
    public void SaveAndLoad_RoundTrip_PreservesAllData()
    {
        var participants = new[] { "Alice", "Bob", "Charlie" };
        var config = new SessionConfiguration(participants, 15);

        _folderHandler.SaveAsRecentSession(config);
        var loaded = _folderHandler.LoadRecentSession("Alice_Bob_Charlie");

        Assert.Equal(participants, loaded.Participants);
        Assert.Equal(15, loaded.SessionLength);
    }

    [Fact]
    public void SaveAsRecentSession_OverwritesExistingSession()
    {
        var first = new SessionConfiguration(new[] { "Alice" }, 5);
        var second = new SessionConfiguration(new[] { "Alice" }, 10);

        _folderHandler.SaveAsRecentSession(first);
        _folderHandler.SaveAsRecentSession(second);
        var loaded = _folderHandler.LoadRecentSession("Alice");

        Assert.Equal(10, loaded.SessionLength);
    }

    [Fact]
    public void GetRecentSessionNames_MultipleSessions_ReturnsAll()
    {
        _folderHandler.SaveAsRecentSession(new SessionConfiguration(new[] { "Alice" }, 5));
        _folderHandler.SaveAsRecentSession(new SessionConfiguration(new[] { "Bob" }, 7));
        _folderHandler.SaveAsRecentSession(new SessionConfiguration(new[] { "Charlie" }, 3));

        var names = _folderHandler.GetRecentSessionNames();

        Assert.Equal(3, names.Length);
        Assert.Contains("Alice", names);
        Assert.Contains("Bob", names);
        Assert.Contains("Charlie", names);
    }

    [Fact]
    public void LoadRecentSession_NonexistentSession_ThrowsException()
    {
        Assert.ThrowsAny<Exception>(() =>
            _folderHandler.LoadRecentSession("DoesNotExist"));
    }

    [Fact]
    public void SaveAsRecentSession_CreatesFolder_WhenNotExists()
    {
        var subDir = Path.Combine(_tempDir, "subfolder");
        var handler = new SessionConfigurationFolderHandler(subDir, _fileHandler);
        var config = new SessionConfiguration(new[] { "Alice" }, 7);

        handler.SaveAsRecentSession(config);

        Assert.True(Directory.Exists(subDir));
        Assert.True(File.Exists(Path.Combine(subDir, "Alice.vpcsession")));
    }

    [Fact]
    public void Constructor_WithEmptyPath_GetRecentSessionNamesReturnsEmpty()
    {
        var handler = new SessionConfigurationFolderHandler("", _fileHandler);

        var result = handler.GetRecentSessionNames();

        Assert.Empty(result);
    }

    [Fact]
    public void Constructor_WithEmptyPath_SaveDoesNotThrow()
    {
        var handler = new SessionConfigurationFolderHandler("", _fileHandler);
        var config = new SessionConfiguration(new[] { "Alice" }, 7);

        // Should not throw — just silently return
        handler.SaveAsRecentSession(config);
    }
}
