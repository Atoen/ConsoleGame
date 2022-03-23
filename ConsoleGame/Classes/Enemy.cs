using ConsoleGame.Structs;

namespace ConsoleGame.Classes;

public class Enemy : GameObject
{
    private static readonly char[] Sprite = {'<', 'O', '>'};
    private const char ProjectileSymbol = '|';
    private const ConsoleColor ProjectileColor = ConsoleColor.Red;
    private const float Speed = 0.5f;
    private const int AttackDelay = 10;
    
    private readonly EnemyGroupSynchronizer _groupSynchronizer;
    private int _attackCd = AttackDelay;

    public static int Score => 100;

    public Enemy(int posX, int posY, EnemyGroupSynchronizer groupSynchronizer, int health = 2) : base(posX, posY)
    {
        _groupSynchronizer = groupSynchronizer;
        Pos.X = posX;
        Pos.Y = posY;
        
        Health = health;
        Color = ConsoleColor.Yellow;
    }
    
    public void Move()
    {
        Clear();
        
        switch (_groupSynchronizer.Direction)
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
                throw new ArgumentOutOfRangeException();
        }
        
        if (_attackCd < 1)
        {
            Attack();
            return;
        }

        _attackCd--;
    }

    private void Attack()
    {
        _attackCd = AttackDelay;
        
        var info = new ProjectileInfo(Pos.X, Pos.Y)
        {
            Symbol = ProjectileSymbol,
            Color = ProjectileColor,
            Hostile = true,
            Direction = ProjectileDirection.Down
        };
        
        ObjectManager.Add(new Projectile(info));
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
