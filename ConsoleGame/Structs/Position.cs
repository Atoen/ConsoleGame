namespace ConsoleGame.Structs;

public struct Position
{
    public int X { get; set; }
    public int Y { get; set; }

    private float _fractionalX;
    private float _fractionalY;
    
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

    public static implicit operator Position((int x, int y) value)
    {
        var (x, y) = value;
        Position pos = new()
        {
            X = x,
            Y = y
        };

        return pos;
    }
    
    public void AddFraction(float floatX, float floatY)
    {
        _fractionalX += floatX;
        _fractionalY += floatY;

        switch (this)
        {
            case {_fractionalX: >= 1}:
                X++;
                _fractionalX--;
                break;
            
            case {_fractionalX: <= -1}:
                X--;
                _fractionalX++;
                break;
            
            case {_fractionalY: >= 1}:
                Y++;
                _fractionalY--;
                break;
            
            case {_fractionalY: <= -1}:
                Y--;
                _fractionalY++;
                break;
        }
    }
}