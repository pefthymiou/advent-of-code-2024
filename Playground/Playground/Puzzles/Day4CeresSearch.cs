namespace Playground.Puzzles;

public class Day4CeresSearch
{
    public static int CountXmas(string input)
    {
        const string word = "XMAS";

        int[][] directions = [[0, 1], [1, 0], [1, 1], [1, -1], [0, -1], [-1, 0], [-1, -1], [-1, 1]];
        var grid = input.Split(Environment.NewLine,
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var columns = grid[0].Length;
        var rows = grid.Length;
        var length = word.Length;
        var count = 0;

        for (var r = 0; r < rows; r++)
        {
            for (var c = 0; c < columns; c++)
            {
                foreach (var direction in directions)
                {
                    var rowDirection = direction[0];
                    var colDirection = direction[1];
                    var row = r;
                    var column = c;

                    int i;

                    for (i = 0; i < length; i++)
                    {
                        if (row < 0 || row >= rows || column < 0 || column >= columns || word[i] != grid[row][column])
                        {
                            break;
                        }

                        row += rowDirection;
                        column += colDirection;
                    }

                    if (i == length)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    public static int CountMasInXmas(string input)
    {
        var grid = input.Split(Environment.NewLine,
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var columns = grid[0].Length;
        var rows = grid.Length;
        var count = 0;

        for (var row = 0; row < rows; row++)
        {
            for (var column = 0; column < columns; column++)
            {
                if (grid[row][column] != 'A')
                {
                    continue;
                }

                if (row - 1 < 0 || row + 1 >= rows || column - 1 < 0 || column + 1 >= columns)
                {
                    continue;
                }

                var r = row;
                var c = column;
                var value = string.Create(4, grid, (span, strings) =>
                {
                    span[0] = strings[r - 1][c - 1];
                    span[1] = strings[r - 1][c + 1];
                    span[2] = strings[r + 1][c + 1];
                    span[3] = strings[r + 1][c - 1];
                });

                if (value is "MSSM" or "MMSS" or "SMMS" or "SSMM")
                {
                    count++;
                }
            }
        }

        return count;
    }
}