namespace ConsoleGame.Classes;

public class EnemyGroup
{
    private readonly int _attackDelay;
    private readonly List<Enemy> _enemies = new();
    
    private int _attackCd;
    private int _groupDirection = 1;
    
    public int StartX;
    public int StartY;
    
    public EnemyGroup(int width, int height)
    {
        StartX = 0;
        StartY = 0;

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                _enemies.Add(new Enemy(i));
            }
        }
    }
}