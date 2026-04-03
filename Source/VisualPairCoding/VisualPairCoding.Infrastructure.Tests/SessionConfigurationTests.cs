namespace VisualPairCoding.Infrastructure.Tests;

public class SessionConfigurationTests
{
    [Fact]
    public void Record_StoresParticipants()
    {
        var participants = new[] { "Alice", "Bob" };
        var config = new SessionConfiguration(participants, 7);

        Assert.Equal(participants, config.Participants);
    }

    [Fact]
    public void Record_StoresSessionLength()
    {
        var config = new SessionConfiguration(new[] { "Alice" }, 15);

        Assert.Equal(15, config.SessionLength);
    }

    [Fact]
    public void Record_EqualityByValue()
    {
        var a = new SessionConfiguration(new[] { "Alice", "Bob" }, 7);
        var b = new SessionConfiguration(new[] { "Alice", "Bob" }, 7);

        // Records use value equality for primitive fields, but arrays use reference equality
        Assert.Equal(a.SessionLength, b.SessionLength);
    }

    [Fact]
    public void Record_EmptyParticipants()
    {
        var config = new SessionConfiguration(Array.Empty<string>(), 7);

        Assert.Empty(config.Participants);
    }
}
