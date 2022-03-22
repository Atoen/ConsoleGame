using System.Diagnostics.SymbolStore;
using System.Security.Cryptography;
using ConsoleGame.Interfaces;

namespace ConsoleGame.Classes;

public static class ObjectManager
{
    private static readonly List<IRemovable> Removables = new();

    private static readonly List<Projectile> Projectiles = new();
    private static readonly List<Obstacle> Obstacles = new();
    private static readonly List<Enemy> Enemies = new();
    private static readonly List<EnemyGroup> EnemyGroups = new();

    private static int _backgroundTicks;

    static ObjectManager()
    {
        Enemy.EnemyEvent += OnEnemyEvent;
    }

    private static void OnEnemyEvent(object? sender, EventArgs e)
    {
        if (sender is null) throw new NullReferenceException();

        foreach (var enemy in Enemies)
        {
            enemy.MoveDown();
        }
        
        (sender as EnemyGroupSynchronizer)?.ChangeDirection();
    }
    
    public static void Update()
    {
        foreach (var projectile in Projectiles)
        {
            projectile.Move();

            if (!CheckCollision(projectile)) continue;
            MarkForRemoval(projectile);
        }

        if (_backgroundTicks % 5 == 0)
        {
            foreach (var enemy in Enemies)
                // Przerywanie pętli gdy następuje zmiana kierunku grupy
                if (!enemy.Move()) break;

            _backgroundTicks = 0;
        }
        
        foreach (var enemy in Enemies) enemy.Draw();

        foreach (var obstacle in Obstacles) obstacle.Draw();

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
                    enemy.Clear();
                    Enemies.Remove(enemy);
                    break;
            }
        }
        
        Removables.Clear();
    }

    private static bool CheckCollision(Projectile projectile)
    {
        foreach (var obstacle in Obstacles)
        {
            if (obstacle.HitBox(projectile)) return true;
        }

        foreach (var enemy in Enemies)
        {
            if (enemy.HitBox(projectile)) return true;
        }

        return false;
    }
}