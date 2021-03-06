using ConsoleGame.Classes.GameObjects;
using ConsoleGame.Classes.GameObjects.Enemies;
using ConsoleGame.Classes.GameObjects.Projectiles;
using ConsoleGame.Interfaces;

namespace ConsoleGame.Classes;

public static class ObjectManager
{
    private static readonly List<IRemovable> Removables = new();

    private static readonly List<Projectile> Projectiles = new();
    private static readonly List<Obstacle> Obstacles = new();
    private static readonly List<EnemyBase> Enemies = new();
    private static readonly List<EnemyGroup> EnemyGroups = new();

    public static readonly Player Player = new();

    private const int EnemyMoveDelay = 5;
    private static int _backgroundTicks;
    public static event EventHandler<GameOverArgs>? GameOverEvent;
        
    public static void Update()
    {
        Player.PerformAction(Input.Get);
        
        // ReSharper disable once ForCanBeConvertedToForeach
        // Nie może być foreach bo SpecialProjectile tworzy nowe pociski
        for (var i = 0; i < Projectiles.Count; i++)
        {
            var projectile = Projectiles[i];
            projectile.Move();

            if (!CheckCollision(projectile)) continue;
            projectile.Remove();
        }

        if (_backgroundTicks % EnemyMoveDelay == 0)
        {
            MoveEnemies();
            _backgroundTicks = 0;
        }
        
        foreach (var enemy in Enemies) enemy.Draw();

        foreach (var obstacle in Obstacles) obstacle.Draw();

        Player.Draw();

        _backgroundTicks++;
    }

    public static void Add(Projectile projectile)
    {
        Projectiles.Add(projectile);
    }

    public static void Add(Obstacle obstacle)
    {
        Obstacles.Add(obstacle);
    }

    public static void Add<T>(EnemyGroup enemyGroup) where T : EnemyBase
    {
        Func<int, int, EnemyBase> enemyFactory;
        
        var startX = enemyGroup.StartX;
        var startY = enemyGroup.StartY;
        int spacingX;
        int spacingY;

        switch (typeof(T))
        {
            case var _ when typeof(T) == typeof(FastEnemy):
                enemyFactory = (x, y) => new FastEnemy(x, y);
                spacingX = FastEnemy.Width;
                spacingY = FastEnemy.Height;
                break;
            
            case var _ when typeof(T) == typeof(TankEnemy):
                enemyFactory = (x, y) => new TankEnemy(x, y);
                spacingX = TankEnemy.Width;
                spacingY = TankEnemy.Height;
                break;
            
            default:
                enemyFactory = (x, y) => new RegularEnemy(x, y);
                spacingX = RegularEnemy.Width;
                spacingY = RegularEnemy.Height;
                break;
        }
        
        
        for (var i = 0; i < enemyGroup.Width; i++)
        for (var j = 0; j < enemyGroup.Height; j++)
        {
            var enemy = enemyFactory(startX + i * spacingX, startY + j * spacingY);
            
            Enemies.Add(enemy);
            enemyGroup.Enemies.Add(enemy);
        }
        
        enemyGroup.Init();
        
        EnemyGroups.Add(enemyGroup);
    }
    
    public static void MarkForRemoval(IRemovable item)
    {
        Removables.Add(item);
    }

    public static void CleanUp()
    {
        foreach (var removable in Removables)
        {
            switch (removable)
            {
                case Projectile projectile:
                    Projectiles.Remove(projectile);
                    break;

                case Obstacle obstacle:
                    obstacle.Clear();
                    Obstacles.Remove(obstacle);
                    break;

                case EnemyBase enemy:
                    Game.Score += enemy.Score;
                    enemy.Clear();
                    Enemies.Remove(enemy);
                    break;
                
                case EnemyGroup enemyGroup:
                    EnemyGroups.Remove(enemyGroup);
                    
                    if (EnemyGroups.Count == 0)
                    {
                        GameOverEvent?.Invoke(null, new GameOverArgs(true));
                    }
                    
                    break;
            }
        }
        
        Removables.Clear();
    }

    private static void MoveEnemies()
    {
        foreach (var enemyGroup in EnemyGroups)
        {
            enemyGroup.Move();
        }
    }

    private static bool CheckCollision(Projectile projectile)
    {
        foreach (var obstacle in Obstacles)
        {
            if (obstacle.HitBox(ref projectile)) return true;
        }

        if (projectile.Hostile)
        {
            if (!Player.HitBox(ref projectile)) return false;
            if (Player.CurrentHealth < 1) GameOverEvent?.Invoke(null, new GameOverArgs(false));
            return true;

        }

        foreach (var enemy in Enemies)
        {
            if (enemy.HitBox(ref projectile)) return true;
        }

        return false;
    }
}

public class GameOverArgs : EventArgs
{
    public bool Won { get; }
    
    public GameOverArgs(bool won)
    {
        Won = won;
    }
}
