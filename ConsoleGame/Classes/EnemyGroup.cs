using ConsoleGame.Structs;

namespace ConsoleGame.Classes;

public struct EnemyGroup
{
    public int StartX;
    public int StartY;
    public readonly int Width;
    public readonly int Height;

    public readonly EnemyGroupSynchronizer Synchronizer = new();

    private Position _upperLeftCorner;
    private Position _lowerRightCorner;

    public EnemyGroup(int width, int height, EnemyDirection direction = EnemyDirection.Right)
    {
        Width = width;
        Height = height;
        StartX = 0;
        StartY = 0;
        
        Synchronizer.Direction = direction;

        _upperLeftCorner = (StartX, StartY);
        _lowerRightCorner = (StartX + width * 4, StartY + Height * 2);
    }

    public void Move()
    {
        
        
        switch (Synchronizer)
        {
            case {Direction: EnemyDirection.Left}:
                _upperLeftCorner.X--;
                _lowerRightCorner.X--;
                break;

            case {Direction: EnemyDirection.Right}:
                _upperLeftCorner.X++;
                _lowerRightCorner.X++;
                break;
        }
    }

    public void ChangeDirection()
    {
        if (Synchronizer.Direction == EnemyDirection.Left)
        {
            Synchronizer.Direction = EnemyDirection.Right;
            return;
        }

        Synchronizer.Direction = EnemyDirection.Left;
    }
}

public class EnemyGroupSynchronizer
{
    public EnemyDirection Direction;
    
    public void ChangeDirection()
    {
        if (Direction == EnemyDirection.Left)
        {
            Direction = EnemyDirection.Right;
            return;
        }
        
        Direction = EnemyDirection.Left;
    }
}