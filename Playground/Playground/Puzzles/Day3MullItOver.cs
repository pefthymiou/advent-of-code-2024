using System.Text.RegularExpressions;

namespace Playground.Puzzles;

public partial class Day3MullItOver
{
    private readonly Regex _mulRegex = MulRegex();
    private readonly Regex _enabledMulRegex = EnabledMulRegex();

    public int SumOfMulInstructions(string input)
    {
        List<(int, int)> instructions = [];
        foreach (Match match in _mulRegex.Matches(input))
        {
            var (x, y) = ParseInstructions(match);

            instructions.Add((x, y));
        }

        return instructions.Sum(x => x.Item1 * x.Item2);
    }

    public int SumOfEnabledMulInstructions(string input)
    {
        List<(int, int)> instructions = [];
        var enabled = true;
        foreach (Match match in _enabledMulRegex.Matches(input))
        {
            switch (match.Value)
            {
                case "do()":
                    enabled = true;
                    break;
                case "don't()":
                    enabled = false;
                    break;
                default:
                    if (enabled) instructions.Add(ParseInstructions(match));
                    break;
            }
        }

        return instructions.Sum(x => x.Item1 * x.Item2);
    }

    private static (int x, int y) ParseInstructions(Match match)
    {
        var value = match.Value;
        var openParenthesis = value.IndexOf('(');
        var comma = value.IndexOf(',', openParenthesis);
        var closeParenthesis = value.IndexOf(')', comma);

        var x = int.Parse(value.AsSpan(openParenthesis + 1, comma - openParenthesis - 1));
        var y = int.Parse(value.AsSpan(comma + 1, closeParenthesis - comma - 1));

        return (x, y);
    }

    [GeneratedRegex(@"mul\(\d+,\d+\)", RegexOptions.Multiline | RegexOptions.Compiled)]
    private static partial Regex MulRegex();

    [GeneratedRegex(@"mul\(\d+,\d+\)|(do\(\))|(don't\(\))", RegexOptions.Multiline | RegexOptions.Compiled)]
    private static partial Regex EnabledMulRegex();
}