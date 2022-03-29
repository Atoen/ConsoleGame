using ConsoleGame.Classes.GameObjects.Projectiles;
using ConsoleGame.Structs;

namespace ConsoleGame.Classes.GameObjects.Enemies;

public class TankEnemy : EnemyBase
{
    private static readonly char[,] Sprite =
    {
        {']', '#', '['},
        {'[', 'U', ']'},
        {'<', '_', '>'}
    };

    public const int Width = 6;
    public const int Height = 4;

    public TankEnemy(int posX, int posY) : base(posX, posY)
    {
        Health = 3;
        Color = ConsoleColor.DarkRed;
        Speed = 0.25f;
        
        ProjectileSettings = new ProjectileInfo
        {
            Symbol = '*',
            Color = ConsoleColor.White,
            
            Speed = 0.25f,
            Damage = 5,
            Direction = ProjectileDirection.Down,
            
            Hostile = true,
        };
        
        AttackDelay = 20;
        AttackCd = AttackDelay;

        Score = 500;
    }
    
    public override bool HitBox(ref Projectile projectile)
    {
        if (!(MathF.Abs(Pos.Y - projectile.Position.Y) <= 1) ||
            !(MathF.Abs(Pos.X - projectile.Position.X) <= 1)) return false;
        
        Hit(projectile.Damage);
        return true;
    }
    
    public override void Draw()
    {
        for (var i = 0; i < Sprite.GetLength(0); i++)
        {
            Display.Print(Pos.X + i - 1, Pos.Y - 1, Sprite[0, i], Color);
            Display.Print(Pos.X + i - 1, Pos.Y, Sprite[1, i], Color);
            Display.Print(Pos.X + i - 1, Pos.Y + 1, Sprite[2, i], Color);
        }
    }

    public override void Clear()
    {
        for (var i = 0; i < Sprite.GetLength(0); i++)
        {
            Display.ClearAt(Pos.X + i - 1, Pos.Y + 1);
            Display.ClearAt(Pos.X + i - 1, Pos.Y);
            Display.ClearAt(Pos.X + i - 1, Pos.Y - 1);
        }
    }
}