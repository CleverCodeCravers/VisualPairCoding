namespace VisualPairCoding.Infrastructure;

public record SessionConfiguration(
    string[] Participants,
    int SessionLength
);