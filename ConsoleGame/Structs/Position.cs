namespace ConsoleGame.Structs;

public struct Position
{
    public int X { get; set; }
    public int Y { get; set; }
    
    public bool Equals(Position other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is Position other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    public static bool operator ==(Position left, Position right)
    {
        return left.X == right.X && left.Y == right.Y;
    }

    public static bool operator !=(Position left, Position right)
    {
        return !(left == right);
    }
}