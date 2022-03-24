using ConsoleGame.Classes.GameObjects;
using ConsoleGame.Interfaces;

namespace ConsoleGame.Classes;

public static class ObjectManager
{
    private static readonly List<IRemovable> Removables = new();

    private static readonly List<Projectile> Projectiles = new();
    private static readonly List<Obstacle> Obstacles = new();
    private static readonly List<Enemy> Enemies = new();
    private static readonly List<EnemyGroup> EnemyGroups = new();

    private static readonly Player Player = new();

    private const int EnemyMoveDelay = 5;
    private static int _backgroundTicks;

    public static int PlayerHealth => Player.CurrentHealth;
    
    public static void Update()
    {
        Player.PerformAction(Input.Get);
        
        foreach (var projectile in Projectiles)
        {
            projectile.Move();

            if (!CheckCollision(projectile)) continue;
            MarkForRemoval(projectile);
        }

        if (_backgroundTicks % EnemyMoveDelay == 0)
        {
            MoveEnemies();
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

    public static void Add(EnemyGroup enemyGroup)
    {
        EnemyGroups.Add(enemyGroup);

        var startX = enemyGroup.StartX;
        var startY = enemyGroup.StartY;
        
        for (var i = 0; i < enemyGroup.Width; i++)
        for (var j = 0; j < enemyGroup.Height; j++)
        {
            Enemies.Add(new Enemy(startX + i * 4, startY + j * 2, enemyGroup.Synchronizer));
        }
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

                case Enemy enemy:

                    Game.Score += Enemy.Score;
                    
                    enemy.Clear();
                    Enemies.Remove(enemy);
                    break;
            }
        }
        
        Removables.Clear();
    }

    private static void MoveEnemies()
    {
        foreach (var enemyGroup in EnemyGroups)
        {
            if (enemyGroup.CheckBoundary())
                enemyGroup.Synchronizer.Direction = EnemyDirection.Down;
        }

        foreach (var enemy in Enemies) enemy.Move();

        _backgroundTicks = 0;
    }

    private static bool CheckCollision(Projectile projectile)
    {
        foreach (var obstacle in Obstacles)
        {
            if (obstacle.HitBox(ref projectile)) return true;
        }

        if (projectile.Hostile)
        {
            if (Player.HitBox(ref projectile)) return true;
        }

        else
        {
            foreach (var enemy in Enemies)
            {
                if (enemy.HitBox(ref projectile)) return true;
            }
        }
        
        return false;
    }
}
