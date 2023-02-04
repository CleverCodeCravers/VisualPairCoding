namespace VisualPairCoding.Infrastructure;

public record SessionConfiguration(
    List<string> Participants,
    int SessionLength
);