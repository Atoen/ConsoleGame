using ConsoleGame.Structs;

namespace ConsoleGame.Classes;

public class Projectile : GameObject
{
    private readonly float _speed;
    private readonly ProjectileDirection _direction;

    public readonly int Damage;
    public readonly bool Hostile;
    public Position Position => Pos;
    public Projectile(ProjectileInfo info) : base(info.PosX, info.PosY)
    {
        Pos.X = info.PosX;
        Pos.Y = info.PosY;
        
        Hostile = info.Hostile;
        Damage = info.Damage;
        Symbol = info.Symbol;
        Color = info.Color;

        _speed = info.Speed;
        _direction = info.Direction;
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
                
                Pos.AddFraction(0, _speed);
                break;

            case ProjectileDirection.Up:
                if (Pos.Y <= 0)
                {
                    Remove();
                    return;
                }
                
                Pos.AddFraction(0, -_speed);
                break;

            case ProjectileDirection.Left:
                if (Pos.X <= 0)
                {
                    Remove();
                    return;
                }

                Pos.AddFraction(-_speed, 0);
                break;

            case ProjectileDirection.Right:
                if (Pos.X >= Game.GameScreenWidth - 1)
                {
                    Remove();
                    return;
                }
                
                Pos.AddFraction(_speed, 0);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        Draw();
    }
}
