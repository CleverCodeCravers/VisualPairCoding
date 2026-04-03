namespace VisualPairCoding.BL.Tests;

public class PairCodingSessionTests
{
    [Fact]
    public void Constructor_SetsParticipants()
    {
        var participants = new[] { "Alice", "Bob" };
        var session = new PairCodingSession(participants, 7, TimeSpan.FromMinutes(60));

        Assert.Equal(participants, session.Participants);
    }

    [Fact]
    public void Constructor_SetsMinutesPerTurn()
    {
        var session = new PairCodingSession(new[] { "Alice" }, 5, TimeSpan.Zero);

        Assert.Equal(5, session.MinutesPerTurn);
    }

    [Fact]
    public void Constructor_SetsTotalDuration()
    {
        var duration = TimeSpan.FromMinutes(90);
        var session = new PairCodingSession(new[] { "Alice" }, 7, duration);

        Assert.Equal(duration, session.TotalDuration);
    }

    [Fact]
    public void Validate_WithNoParticipants_ReturnsErrorMessage()
    {
        var session = new PairCodingSession(Array.Empty<string>(), 7, TimeSpan.Zero);

        var result = session.Validate();

        Assert.NotNull(result);
        Assert.Contains("at least one participant", result);
    }

    [Fact]
    public void Validate_WithOneParticipant_ReturnsNull()
    {
        var session = new PairCodingSession(new[] { "Alice" }, 7, TimeSpan.Zero);

        var result = session.Validate();

        Assert.Null(result);
    }

    [Fact]
    public void Validate_WithMultipleParticipants_ReturnsNull()
    {
        var session = new PairCodingSession(new[] { "Alice", "Bob", "Charlie" }, 7, TimeSpan.Zero);

        var result = session.Validate();

        Assert.Null(result);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(7)]
    [InlineData(30)]
    public void Constructor_AcceptsVariousMinutesPerTurn(int minutes)
    {
        var session = new PairCodingSession(new[] { "Alice" }, minutes, TimeSpan.Zero);

        Assert.Equal(minutes, session.MinutesPerTurn);
    }

    [Fact]
    public void Validate_WithTenParticipants_ReturnsNull()
    {
        var participants = Enumerable.Range(1, 10).Select(i => $"Person{i}").ToArray();
        var session = new PairCodingSession(participants, 7, TimeSpan.Zero);

        var result = session.Validate();

        Assert.Null(result);
    }
}
