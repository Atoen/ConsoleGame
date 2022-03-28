using ConsoleGame.Structs;

namespace ConsoleGame.Classes.GameObjects.Projectiles;

public class ExplodingProjectile : Projectile
{
    private const int TriggerRadius = 2;
    private const int ExplosionDamage = 5;

    public ExplodingProjectile(ProjectileInfo info) : base(info)
    {
    }
    
    public override void Move()
    {
        if (MathF.Abs(Pos.Y - ObjectManager.Player.Position.Y) <= TriggerRadius &&
            MathF.Abs(Pos.X - ObjectManager.Player.Position.X) <= TriggerRadius)
        {
            Explode();
            return;
        }
        
        base.Move();
    }

    private void Explode()
    {
        var info = new ProjectileInfo(Pos.X, Pos.Y)
        {
            Speed = 0,
            Damage = ExplosionDamage,
            Hostile = Hostile,
            Symbol = '*',
            Color = ConsoleColor.DarkYellow
        };

        ObjectManager.Add(new Explosion(info));
        
        Remove();
    }
}