namespace ConsoleGame.Classes;

public struct EnemyGroup
{
    public int StartX;
    public int StartY;
    public int Width;
    public int Height;

    public EnemyGroupSynchronizer Synchronizer;

    public EnemyGroup(int width, int height, EnemyDirection direction = EnemyDirection.Right)
    {
        Width = width;
        Height = height;
        StartX = 0;
        StartY = 0;
        
        Synchronizer.Direction = direction;
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

public struct EnemyGroupSynchronizer
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