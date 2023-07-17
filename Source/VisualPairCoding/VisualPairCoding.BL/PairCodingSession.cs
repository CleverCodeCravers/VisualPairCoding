namespace VisualPairCoding.BL;

public class PairCodingSession
{
    public string[] Participants { get; }

    public int MinutesPerTurn { get; }

    public TimeSpan TotalDuration { get; }

    public PairCodingSession(string[] participants, int minutesPerTurn, TimeSpan totalDuration)
    {
        Participants = participants;
        MinutesPerTurn = minutesPerTurn;
        TotalDuration = totalDuration;
    }

    public string? Validate()
    {
        if (Participants.Length == 0)
        {
            return "You need at least one participant. ( Although that setting might still be a little bit boring. :) )";
        }

        return null;
    }
}
