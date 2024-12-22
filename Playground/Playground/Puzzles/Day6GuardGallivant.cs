namespace Playground.Puzzles;

public class Day6GuardGallivant
{
    private static readonly (int Row, int Column)[] DirectionOffsets =
    [
        (-1, 0), // Up
        (0, 1), // Right
        (1, 0), // Down
        (0, -1) // Left
    ];

    public static int CalculateDistinctPositions(string input)
    {
        var grid = ParseInput(input);
        var guard = FindGuard(grid) ?? new Guard(new Position(0, 0), Direction.Up);
        var rows = grid.GetLength(0);
        var columns = grid.GetLength(1);

        HashSet<Position> visited = [];
        HashSet<string> seen = [];

        while (true)
        {
            if (IsInBounds(guard.Position, rows, columns))
            {
                visited.Add(guard.Position);
            }

            if (!seen.Add(guard.ToString()))
            {
                break;
            }

            if (HasObstacleAhead(guard, grid, rows, columns))
            {
                guard.Direction = (Direction)(((int)guard.Direction + 1) % 4);
            }
            else
            {
                var offset = DirectionOffsets[(int)guard.Direction];
                var nextPosition = guard.Position.Move(offset);

                if (!IsInBounds(nextPosition, rows, columns))
                {
                    break;
                }

                guard.Move(offset);
            }
        }

        return visited.Count;
    }

    private static Guard? FindGuard(char[,] grid)
    {
        for (var row = 0; row < grid.GetLength(0); row++)
        {
            for (var column = 0; column < grid.GetLength(1); column++)
            {
                if (grid[row, column] == '^')
                {
                    return new Guard(new Position(row, column), Direction.Up);
                }
            }
        }

        return null;
    }

    private static bool HasObstacleAhead(Guard guard, char[,] grid, int rows, int columns)
    {
        var offset = DirectionOffsets[(int)guard.Direction];
        var nextPosition = guard.Position.Move(offset);

        return !IsInBounds(nextPosition, rows, columns) || grid[nextPosition.Row, nextPosition.Column] == '#';
    }

    private static bool IsInBounds(Position position, int rows, int columns) =>
        position.Row >= 0 && position.Row < rows && position.Column >= 0 && position.Column < columns;

    private static char[,] ParseInput(string input)
    {
        var lines = input.Split(Environment.NewLine,
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        var rows = lines.Length;
        var cols = lines[0].Length;
        var grid = new char[rows, cols];

        for (var i = 0; i < rows; i++)
        {
            for (var j = 0; j < cols; j++)
            {
                grid[i, j] = lines[i][j];
            }
        }

        return grid;
    }
}

public sealed class Position
{
    public int Row { get; }
    public int Column { get; }

    public Position(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public Position Move((int Row, int Col) offset) => new(Row + offset.Row, Column + offset.Col);

    public override bool Equals(object? obj) =>
        obj is Position other && Row == other.Row && Column == other.Column;

    public override int GetHashCode() => HashCode.Combine(Row, Column);

    public override string ToString() => $"{Row},{Column}";
}

public sealed class Guard
{
    public Position Position { get; private set; }
    public Direction Direction { get; set; }

    public Guard(Position position, Direction direction)
    {
        Position = position;
        Direction = direction;
    }

    public void Move((int Row, int Column) offset) => Position = Position.Move(offset);

    public override bool Equals(object? obj) =>
        obj is Guard other && Position.Equals(other.Position) && Direction == other.Direction;

    public override int GetHashCode() => HashCode.Combine(Position, Direction);

    public override string ToString() => $"{Position}-{Direction}";

    public Guard Clone() => new(Position, Direction);
}

public enum Direction
{
    Up,
    Right,
    Down,
    Left
};