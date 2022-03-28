using ConsoleGame.Structs;

namespace ConsoleGame.Classes.GameObjects.Projectiles;

public class Explosion : Projectile
{
    private static readonly char[,] Sprite =
    {
        {' ', (char) 176, (char) 176, ' '},
        {(char) 176, (char) 178, (char) 178, (char) 176},   // 176 ▒
        {(char) 176, (char) 178, (char) 178, (char) 176},   // 178░█
        {' ', (char) 176, (char) 176, ' '}                  
    };
    
    private const int Radius = 2;
    private const int LifeTime = 10;
    
    private readonly List<Projectile> _explosionProjectiles = new();
    private int _lifetime;

    public Explosion(ProjectileInfo info) : base(info)
    {
        _lifetime = LifeTime;
        var center = info.Pos;

        for (var i = -Radius; i < Radius; i++)
        for (var j = -Radius; j < Radius; j++)
        {
            var currentSymbol = Sprite[i + Radius, j + Radius];
            
            if (currentSymbol == ' ') continue;
            
            info.Pos = (center.X + i, center.Y + j);
            info.Symbol = currentSymbol;
            
            var explosionProjectile = new Projectile(info);
            _explosionProjectiles.Add(explosionProjectile);
            ObjectManager.Add(explosionProjectile);
        }
    }

    public override void Move()
    {
        if (_lifetime <= 0)
        {
            Remove();
            return;
        }
        
        _lifetime--;
    }

    public override void Remove()
    {
        Clear();
        foreach (var explosionProjectile in _explosionProjectiles)
        {
            explosionProjectile.Remove();
            explosionProjectile.Clear();
        }
        
        _explosionProjectiles.Clear();
        
        base.Remove();
    }
}