using ConsoleGame.Structs;

namespace ConsoleGame.Classes.GameObjects.Projectiles;

public class Projectile : GameObject
{
    protected readonly float Speed;
    protected readonly ProjectileDirection Direction;

    public readonly int Damage;
    public readonly bool Hostile;
    public Position Position => Pos;
    public Projectile(ProjectileInfo info) : base(info.PosX, info.PosY)
    {
        Hostile = info.Hostile;
        Damage = info.Damage;
        Symbol = info.Symbol;
        Color = info.Color;

        Speed = info.Speed;
        Direction = info.Direction;
    }

    public virtual void Disable()
    {
        Remove();
    }

    public virtual void Move()
    {
        Clear();

        switch (Direction)
        {
            case ProjectileDirection.Down:
                if (Pos.Y >= Game.GameScreenHeight - 1)
                {
                    Remove();
                    return;
                }
                
                Pos.AddFraction(0, Speed);
                break;

            case ProjectileDirection.Up:
                if (Pos.Y <= 0)
                {
                    Remove();
                    return;
                }
                
                Pos.AddFraction(0, -Speed);
                break;

            case ProjectileDirection.Left:
                if (Pos.X <= 0)
                {
                    Remove();
                    return;
                }

                Pos.AddFraction(-Speed, 0);
                break;

            case ProjectileDirection.Right:
                if (Pos.X >= Game.GameScreenWidth - 1)
                {
                    Remove();
                    return;
                }
                
                Pos.AddFraction(Speed, 0);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        Draw();
    }
}
