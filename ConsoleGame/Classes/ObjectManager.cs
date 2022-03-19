using ConsoleGame.Interfaces;

namespace ConsoleGame.Classes;

public static class ObjectManager
{
    private static readonly List<IRemovable> Removables = new();

    private static readonly List<Projectile> Projectiles = new();
    private static readonly List<Obstacle> Obstacles = new();

    public static void Update()
    {
        for (var i = 0; i < Projectiles.Count; i++)
        {
            var projectile = Projectiles[i];
            
            projectile.Move();

            if (CheckCollision(ref projectile))
            {
                MarkForRemoval(projectile);
                System.Diagnostics.Debug.WriteLine("marked");
            }
            
            projectile.Draw();
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

    public static void Clear()
    {
        foreach (var removable in Removables)
        {
            switch (removable)
            {
                case Projectile projectile:
                    projectile.Clear();
                    Projectiles.Remove(projectile);
                    break;

                case Obstacle obstacle:
                    Obstacles.Remove(obstacle);
                    break;
            }
        }
        
        Removables.Clear();
    }

    private static bool CheckCollision(ref Projectile projectile)
    {
        foreach (var obstacle in Obstacles)
        {
            if (obstacle.HitBox(projectile)) return true;
        }

        return false;
    }
}