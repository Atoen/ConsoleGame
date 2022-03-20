namespace ConsoleGame.Structs;

public struct Position
{
    public int X { get; set; }
    public int Y { get; set; }

    internal float _x;
    internal float _y;
    
    private bool Equals(Position other)
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
        return left.X != right.X || left.Y != right.Y;
    }
}

public static class PositionExtensionMethods
{
    public static void AddFraction(ref this Position pos, float floatX, float floatY)
    {
        pos._x += floatX;
        pos._y += floatY;

        switch (pos)
        {
            case {_x: >= 1}:
                pos.X++;
                pos._x--;
                break;
            
            case {_x: <= -1}:
                pos.X--;
                pos._x++;
                break;
            
            case {_y: >= 1}:
                pos.Y++;
                pos._y--;
                break;
            
            case {_y: <= -1}:
                pos.Y--;
                pos._y++;
                break;
        }
    }
}