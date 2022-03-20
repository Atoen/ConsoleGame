using ConsoleGame.Interfaces;
using ConsoleGame.Structs;

namespace ConsoleGame.Classes;

public class Enemy : IRemovable
{
    private static readonly char[] Sprite = {'<', 'O', '>'};
    private const char ProjectileSymbol = '|';
    
    private const ConsoleColor Color = ConsoleColor.White;
    private const ConsoleColor ProjectileColor = ConsoleColor.White;

    private const float Speed = 0.5f;
    private int _health = 2;

    public const int Score = 100;
    private Position _pos;

    public Enemy(int posX, int posY)
    {
        _pos.X = posX;
        _pos.Y = posY;
    }

    public void Move(int direction)
    {
        Clear();
        _pos.AddFraction(Speed * direction, 0);
    }

    public void Draw()
    {
        Display.Print(_pos.X - 1, _pos.Y, Sprite[0], Color);
        Display.Print(_pos.X, _pos.Y, Sprite[1], Color);
        Display.Print(_pos.X + 1, _pos.Y, Sprite[2], Color);
    }

    public bool HitBox(Projectile projectile)
    {
        if (projectile.Position.Y != _pos.Y || !(MathF.Abs(projectile.Position.X - _pos.X) <= 1)) return false;

        Hit(projectile.Damage);
        return true;
    }

    private void Hit(int damage)
    {
        _health -= damage;

        if (_health < 1)
        {
            Remove();
        }
    }

    public void Clear()
    {
        Display.ClearAt(_pos.X - 1, _pos.Y);
        Display.ClearAt(_pos.X, _pos.Y);
        Display.ClearAt(_pos.X + 1, _pos.Y);
    }

    public void Remove()
    {
        ObjectManager.MarkForRemoval(this);
    }
}