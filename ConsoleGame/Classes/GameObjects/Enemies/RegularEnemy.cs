using System.Diagnostics;
using ConsoleGame.Classes.GameObjects.Projectiles;
using ConsoleGame.Structs;

namespace ConsoleGame.Classes.GameObjects.Enemies;

public class RegularEnemy : EnemyBase
{
    private static readonly char[] Sprite = {'<', 'O', '>'};

    public const int Width = 4;
    public const int Height = 2;

    public RegularEnemy(int posX, int posY) : base(posX, posY)
    {
        Health = 2;
        Color = ConsoleColor.Yellow;
        Speed = 0.5f;

        ProjectileSettings = new ProjectileInfo
        {
            Symbol = '|',
            Color = ConsoleColor.Red,
            
            Speed = 0.5f,
            Damage = 1,
            Direction = ProjectileDirection.Down,
            
            Hostile = true,
        };

        AttackDelay = 10;
        AttackCd = AttackDelay;

        Score = 100;
    }
    
    public override bool HitBox(ref Projectile projectile)
    {
        if (projectile.Position.Y != Pos.Y || !(MathF.Abs(projectile.Position.X - Pos.X) <= 1)) return false;

        Hit(projectile.Damage);
        return true;
    }
    
    public override void Draw()
    {
        Display.Print(Pos.X - 1, Pos.Y, Sprite[0], Color);
        Display.Print(Pos.X, Pos.Y, Sprite[1], Color);
        Display.Print(Pos.X + 1, Pos.Y, Sprite[2], Color);
    }

    public override void Clear()
    {
        Display.ClearAt(Pos.X - 1, Pos.Y);
        Display.ClearAt(Pos.X, Pos.Y);
        Display.ClearAt(Pos.X + 1, Pos.Y);
    }
}