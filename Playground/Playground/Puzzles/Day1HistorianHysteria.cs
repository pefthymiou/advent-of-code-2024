namespace Playground.Puzzles;

public class Day1HistorianHysteria
{
    public static int CalculateTotalDistance(string[] inputLines)
    {
        List<int> firstLocationIds = [];
        List<int> secondLocationIds = [];
        foreach (var line in inputLines)
        {
            var strings = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            firstLocationIds.Add(int.Parse(strings[0].Trim()));
            secondLocationIds.Add(int.Parse(strings[1].Trim()));
        }

        firstLocationIds.Sort();
        secondLocationIds.Sort();

        var count = firstLocationIds.Count;
        List<int> distances = [];

        for (var i = 0; i < count; i++)
        {
            distances.Add(Math.Abs(firstLocationIds[i] - secondLocationIds[i]));
        }

        return distances.Sum();
    }

    public static int CalculateSimilarityScore(string[] inputLines)
    {
        List<int> firstLocationIds = [];
        List<int> secondLocationIds = [];
        foreach (var line in inputLines)
        {
            var strings = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            firstLocationIds.Add(int.Parse(strings[0].Trim()));
            secondLocationIds.Add(int.Parse(strings[1].Trim()));
        }

        List<int> occurrences = [];
        occurrences.AddRange(firstLocationIds.Select(locationId =>
            secondLocationIds.Count(x => x == locationId) * locationId));

        return occurrences.Sum();
    }
}