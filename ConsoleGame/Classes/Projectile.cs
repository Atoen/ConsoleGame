using ConsoleGame.Interfaces;
using ConsoleGame.Structs;

namespace ConsoleGame.Classes;

public class Projectile : IRemovable
{
    private readonly char _symbol;
    private readonly ConsoleColor _color;
    private readonly float _speed;
    private readonly ProjectileDirection _direction;
    
    public readonly int Damage;
    public readonly bool Hostile;
    public Position Pos;

    public bool IsActive = true;
    
    public Projectile(ProjectileInfo info)
    {
        Pos.X = info.PosX;
        Pos.Y = info.PosY;
        Hostile = info.Hostile;
        Damage = info.Damage;
        
        _speed = info.Speed;
        _symbol = info.Symbol;
        _color = info.Color;
        _direction = info.Direction;
    }

    public void Draw()
    {
        Display.Print(Pos.X, Pos.Y, _symbol, _color);
    }

    public void Move()
    {
        Display.ClearAt(Pos.X, Pos.Y);

        switch (_direction)
        {
            case ProjectileDirection.Down:
                if (Pos.Y >= Game.GameScreenHeight - 1)
                {
                    Remove();
                    return;
                }
                
                Pos.Y += (int) _speed;

                break;

            case ProjectileDirection.Up:
                if (Pos.Y <= 0)
                {
                    Remove();
                    return;
                }

                Pos.Y -= (int) _speed;

                break;

            case ProjectileDirection.Left:
                if (Pos.X <= 0)
                {
                    Remove();
                    return;
                }

                Pos.X -= (int) _speed;

                break;

            case ProjectileDirection.Right:
                if (Pos.X >= Game.GameScreenWidth - 1)
                {
                    Remove();
                    return;
                }

                Pos.X += (int) _speed;

                break;
        }
    }
    
    public void Remove()
    {
        IsActive = false;
        ObjectManager.MarkForRemoval(this);
    }

    public void Clear()
    {
        Display.ClearAt(Pos.X, Pos.Y);
    }
}