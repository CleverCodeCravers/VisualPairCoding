namespace VisualPairCoding.BL;

public class PairCodingSession
{
    public string[] Participants { get; }

    public int MinutesPerRound { get; }

    public PairCodingSession(string[] participants, int minutesPerRound)
    {
        Participants = participants;
        MinutesPerRound = minutesPerRound;
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
