using ConsoleGame.Classes.GameObjects.Projectiles;
using ConsoleGame.Structs;

namespace ConsoleGame.Classes.GameObjects.Enemies;

public abstract class EnemyBase : GameObject
{
    protected float Speed;
    protected int AttackDelay;
    protected int AttackCd;
    protected ProjectileInfo ProjectileSettings = new();

    public int Score = 0;
    
    public bool IsActive = true;
    public bool Attacking = false;
    public Position Position => Pos;

    protected EnemyBase(int posX, int posY) : base(posX, posY)
    {
        Pos.X = posX;
        Pos.Y = posY;
    }

    public void Move(EnemyDirection direction)
    {
        Clear();
        
        switch (direction)
        {
            case EnemyDirection.Left:
                Pos.AddFraction(-Speed, 0);
                break;
        
            case EnemyDirection.Right:
                Pos.AddFraction(Speed, 0);
                break;
            
            case EnemyDirection.Down:
                Pos.Y++;
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(direction));
        }
        
        if (AttackCd < 1)
        {
            Attack();
            return;
        }

        AttackCd--;
    }
    
    protected virtual void Attack()
    {
        if (!Attacking) return;

        AttackCd = AttackDelay;

        ProjectileSettings.Pos = Pos;
        ObjectManager.Add(new Projectile(ProjectileSettings));
    }

    public override void Remove()
    {
        IsActive = false;
        ObjectManager.MarkForRemoval(this);
    }
}

