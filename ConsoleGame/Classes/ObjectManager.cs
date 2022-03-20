﻿using ConsoleGame.Interfaces;

namespace ConsoleGame.Classes;

public static class ObjectManager
{
    private static readonly List<IRemovable> Removables = new();

    private static readonly List<Projectile> Projectiles = new();
    private static readonly List<Obstacle> Obstacles = new();

    public static void Update()
    {
        
        foreach (var projectile in Projectiles)
        {
            projectile.Move();
            
            if (!CheckCollision(projectile)) continue;
            MarkForRemoval(projectile);
        }

        foreach (var obstacle in Obstacles)
        {
            obstacle.Draw();
        }
    }

    public static void Add(Projectile projectile)
    {
        Projectiles.Add(projectile);
    }

    public static void Add(Obstacle obstacle)
    {
        Obstacles.Add(obstacle);
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

        return false;
    }
}