﻿namespace ConsoleGame.Classes;

public class Enemy : GameObject
{
    private static readonly char[] Sprite = {'<', 'O', '>'};
    private const char ProjectileSymbol = '|';
    private const ConsoleColor ProjectileColor = ConsoleColor.White;
    private const float Speed = 0.5f;
    
    private readonly EnemyGroupSynchronizer _groupSynchronizer;
    public static event EventHandler EnemyEvent = delegate {  };

    public static int Score => 100;

    public Enemy(int posX, int posY, EnemyGroupSynchronizer groupSynchronizer, int health = 2) : base(posX, posY)
    {
        _groupSynchronizer = groupSynchronizer;
        Pos.X = posX;
        Pos.Y = posY;
        
        Health = health;
        Color = ConsoleColor.White;
    }
    
    public bool Move()
    {
        Clear();
        switch (_groupSynchronizer.Direction)
        {
            case EnemyDirection.Left when Pos.X > 5:
                Pos.AddFraction(-Speed, 0);
                break;
            
            case EnemyDirection.Right when Pos.X < Game.GameScreenWidth - 5:
                Pos.AddFraction(Speed, 0);
                break;
            
            // Zmiana kierunku
            default:
                // _groupSynchronizer.ChangeDirection();
                EnemyEvent(_groupSynchronizer, EventArgs.Empty);
                return false;
        }

        return true;
    }

    public void MoveDown()
    {
        Clear();
        Pos.Y++;
    }

    public override bool HitBox(Projectile projectile)
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

public enum EnemyDirection
{
    Left,
    Right,
    Down
}
