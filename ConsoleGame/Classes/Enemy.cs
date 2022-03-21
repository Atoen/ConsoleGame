namespace ConsoleGame.Classes;

public class Enemy : GameObject
{
    private static readonly char[] Sprite = {'<', 'O', '>'};
    private const char ProjectileSymbol = '|';
    private const ConsoleColor ProjectileColor = ConsoleColor.White;
    private const float Speed = 0.5f;
    
    private EnemyGroupSynchronizer _groupSynchronizer;
    
    public const int Score = 100;

    public Enemy(int posX, int posY, ref EnemyGroupSynchronizer groupSynchronizer, int health = 2) : base(posX, posY)
    {
        Pos.X = posX;
        Pos.Y = posY;
        
        Health = health;
        Color = ConsoleColor.White;
        _groupSynchronizer = groupSynchronizer;
    }
    
    public void Move()
    {
        Clear();
        switch (_groupSynchronizer.Direction)
        {
            case EnemyDirection.Left when Pos.X > 5:
                Pos.AddFraction(-Speed, 0);
                break;
            
            case EnemyDirection.Right when Pos.Y < Game.GameScreenWidth - 5:
                Pos.AddFraction(Speed, 0);
                break;
            
            default:
                _groupSynchronizer.ChangeDirection();
                break;
        }
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
