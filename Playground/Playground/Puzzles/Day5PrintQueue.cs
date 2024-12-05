namespace Playground.Puzzles;

public class Day5PrintQueue
{
    public static int CalculateSumOfMiddlePageNumbersInOrderedUpdates(string input)
    {
        var (orderingRules, pageUpdates) = ParseInput(input);

        var pagesAfter = FillPagesAfterDictionary(orderingRules, pageUpdates);

        var orderedUpdates = FindOrderedUpdates(pageUpdates, pagesAfter);

        return orderedUpdates.Sum(u => u[u.Length / 2]);
    }

    public static int CalculateSumOfMiddlePageNumbersInUnorderedUpdates(string input)
    {
        var (orderingRules, pageUpdates) = ParseInput(input);

        var pagesAfter = FillPagesAfterDictionary(orderingRules, pageUpdates);

        var unorderedUpdates = pageUpdates.Except(FindOrderedUpdates(pageUpdates, pagesAfter)).ToArray();

        foreach (var update in unorderedUpdates)
        {
            Array.Sort(update, (x, y) =>
            {
                if (pagesAfter.TryGetValue(x, out var pagesAfterXPage) && pagesAfterXPage.Contains(y)) return -1;
                if (pagesAfter.TryGetValue(y, out var pagesAfterYPage) && pagesAfterYPage.Contains(x)) return 1;
                return 0;
            });
        }

        return unorderedUpdates.Sum(u => u[u.Length / 2]);
    }

    private static List<int[]> FindOrderedUpdates(int[][] pageUpdates, Dictionary<int, HashSet<int>> pagesAfter)
    {
        List<int[]> orderedUpdates = [];
        foreach (var pageUpdate in pageUpdates)
        {
            var isOrdered = true;
            HashSet<int> checkedPages = [];
            foreach (var page in pageUpdate)
            {
                if (pagesAfter.TryGetValue(page, out var pagesAfterCurrentPage) &&
                    checkedPages.Any(pagesAfterCurrentPage.Contains))
                {
                    isOrdered = false;
                }

                checkedPages.Add(page);
            }

            if (isOrdered)
            {
                orderedUpdates.Add(pageUpdate);
            }
        }

        return orderedUpdates;
    }

    private static ((int, int)[] orderingRules, int[][] pageUpdates) ParseInput(string input)
    {
        var orderingRules = input[..(input.LastIndexOf('|') + 3)]
            .Trim()
            .Split(Environment.NewLine)
            .Select(x => x.Split('|'))
            .Select(x => (int.Parse(x[0]), int.Parse(x[1])))
            .ToArray();

        var pageUpdates = input[(input.LastIndexOf('|') + 3)..]
            .Trim()
            .Split(Environment.NewLine)
            .Select(x => x.Split(',').Select(int.Parse).ToArray())
            .ToArray();
        return (orderingRules, pageUpdates);
    }

    private static Dictionary<int, HashSet<int>> FillPagesAfterDictionary(
        IReadOnlyCollection<(int, int)> orderingRules,
        IReadOnlyCollection<IReadOnlyCollection<int>> pageUpdates)
    {
        Dictionary<int, HashSet<int>> pagesAfter = [];
        foreach (var pageUpdate in pageUpdates)
        {
            foreach (var page in pageUpdate)
            {
                if (!pagesAfter.TryGetValue(page, out _))
                {
                    pagesAfter[page] = [];
                }

                pagesAfter[page] = orderingRules
                    .Where(r => r.Item1 == page)
                    .Select(x => x.Item2)
                    .ToHashSet();
            }
        }

        return pagesAfter;
    }
}