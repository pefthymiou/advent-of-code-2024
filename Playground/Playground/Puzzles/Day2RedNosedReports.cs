namespace Playground.Puzzles;

public class Day2RedNosedReports
{
    public static int FindSafeReports(string[] input)
    {
        List<int[]> reports = [];
        reports.AddRange(input.Select(line => line.Split(' ').Select(int.Parse).ToArray()));

        return reports.Count(IsReportSafe);
    }

    public static int FindSafeReportsWithDampener(string[] input)
    {
        List<int[]> reports = [];
        reports.AddRange(input.Select(line => line.Split(' ').Select(int.Parse).ToArray()));

        var safeReports = 0;

        foreach (var report in reports)
        {
            if (IsReportSafe(report))
            {
                safeReports++;
            }
            else
            {
                if (report.Where((_, i) => IsReportSafe(report.Where((_, index) => index != i).ToArray())).Any())
                {
                    safeReports++;
                }
            }
        }

        return safeReports;
    }

    private static bool IsReportSafe(int[] levels)
    {
        if (levels.Where((t, i) => i < levels.Length - 1 && Math.Abs(t - levels[i + 1]) is < 1 or > 3).Any())
        {
            return false;
        }

        return levels.SequenceEqual(levels.Order()) || levels.SequenceEqual(levels.OrderDescending());
    }
}