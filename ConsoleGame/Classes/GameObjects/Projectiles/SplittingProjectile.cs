using ConsoleGame.Structs;

namespace ConsoleGame.Classes.GameObjects.Projectiles;

public class SplittingProjectile : Projectile
{
    public SplittingProjectile(ProjectileInfo info) : base(info)
    {
    }

    public override void Move()
    {
        if (Direction == ProjectileDirection.Down && Pos.Y == ObjectManager.Player.Position.Y)
        {
            Split();
            return;
        }
        
        base.Move();
    }

    private void Split()
    {
        ObjectManager.MarkForRemoval(this);

        var info = new ProjectileInfo(Pos.X, Pos.Y)
        {
            Symbol = '<',
            Color = Color,

            Speed = Speed,
            Damage = Damage,
            Direction = ProjectileDirection.Left,

            Hostile = Hostile
        };
        
        ObjectManager.Add(new Projectile(info));

        info.Symbol = '>';
        info.Direction = ProjectileDirection.Right;
        ObjectManager.Add(new Projectile(info));
        
        Clear();
    }
}