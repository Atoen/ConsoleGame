using ConsoleGame.Structs;

namespace ConsoleGame.Classes.GameObjects.Enemies;

public abstract class EnemyBase : GameObject
{
    protected float Speed;
    protected int AttackDelay;
    protected int AttackCd;

    protected char ProjectileSymbol;
    protected ConsoleColor ProjectileColor;

    public int Width = 1;
    public int Height = 1;
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
        }
        
        if (AttackCd < 1)
        {
            Attack();
            return;
        }

        AttackCd--;
    }
    
    private void Attack()
    {
        if (!Attacking) return;

        AttackCd = AttackDelay;
        
        var info = new ProjectileInfo(Pos.X, Pos.Y)
        {
            Symbol = ProjectileSymbol,
            Color = ProjectileColor,
            Hostile = true,
            Direction = ProjectileDirection.Down
        };
        
        ObjectManager.Add(new Projectile(info));
    }
}

