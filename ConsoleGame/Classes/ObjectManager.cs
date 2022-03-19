using ConsoleGame.Interfaces;

namespace ConsoleGame.Classes;

public static class ObjectManager
{
    private static List<IRemovable> _removables = new();

    private static List<Projectile> _projectiles = new();

    public static void Update()
    {
        foreach (var projectile in _projectiles)
        {
            projectile.Move();
            projectile.Draw();
        }
    }

    public static void Add(Projectile projectile)
    {
        _projectiles.Add(projectile);
    }

    public static void Remove(IRemovable item)
    {
        _removables.Add(item);
    }

    public static void Clear()
    {
        _removables.Clear();
    }
}