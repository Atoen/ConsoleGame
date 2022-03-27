using System.Diagnostics;
using ConsoleGame.Classes.GameObjects;
using ConsoleGame.Classes.GameObjects.Enemies;
using ConsoleGame.Interfaces;

namespace ConsoleGame.Classes;

public static class ObjectManager
{
    private static readonly List<IRemovable> Removables = new();

    private static readonly List<Projectile> Projectiles = new();
    private static readonly List<Obstacle> Obstacles = new();
    private static readonly List<EnemyBase> Enemies = new();
    private static readonly List<GenericGroup> GenericGroups = new();

    private static readonly Player Player = new();

    private const int EnemyMoveDelay = 5;
    private static int _backgroundTicks;

    public static int PlayerHealth => Player.CurrentHealth;
    
    public static void Update()
    {
        Player.PerformAction(Input.Get);
        
        // Trace.WriteLine(Projectiles.Count);
        
        // Trace.WriteLine($"Enemy list  {Enemies.Count}");
        // Trace.WriteLine($"Enemy class {Enemy.Count}");
        // Trace.WriteLine($"Enemy group {NewEnemyGroups[0].Enemies.Count}");
        
        foreach (var projectile in Projectiles)
        {
            projectile.Move();

            if (!CheckCollision(projectile)) continue;
            MarkForRemoval(projectile);
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

    public static void Add<T>(GenericGroup genericGroup) where T : EnemyBase
    {

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
                
                case EnemyGroupOld enemyGroup:
                    EnemyGroups.Remove(enemyGroup);
                    break;
            }
        }
        
        Removables.Clear();
    }

    private static void MoveEnemies()
    {
        foreach (var newEnemyGroup in GenericGroups)
        {
            newEnemyGroup.Move();
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
