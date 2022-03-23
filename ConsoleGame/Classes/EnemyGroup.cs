using ConsoleGame.Classes.GameObjects;
using ConsoleGame.Structs;

namespace ConsoleGame.Classes;

public class EnemyGroup
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
        _lowerRightCorner = (StartX + width * 5 - 5, StartY + Height * 2);
    }

    public bool CheckBoundary()
    {
        switch (Synchronizer)
        {
            case {Direction: EnemyDirection.Left} when _upperLeftCorner.X > 1:
                _upperLeftCorner.AddFraction(-0.5f, 0);
                _lowerRightCorner.AddFraction(-0.5f, 0);
                break;

            case {Direction: EnemyDirection.Right} when _lowerRightCorner.X < Game.GameScreenWidth - 1:
                _upperLeftCorner.AddFraction(0.5f, 0);
                _lowerRightCorner.AddFraction(0.5f, 0);
                break;
            
            case {Direction: EnemyDirection.Down}:
                Synchronizer.ResumeMoving();
                break;

            default:
                Synchronizer.ChangeDirection();
                return true;
        }

        return false;
    }
}

public class EnemyGroupSynchronizer
{
    public EnemyDirection Direction;
    private EnemyDirection _directionAfterDown;
    
    public void ChangeDirection()
    {
        Direction = Direction == EnemyDirection.Left ? EnemyDirection.Right : EnemyDirection.Left;

        _directionAfterDown = Direction;
    }

    public void ResumeMoving()
    {
        Direction = _directionAfterDown;
    }
}