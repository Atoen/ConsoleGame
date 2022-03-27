namespace ConsoleGame.Classes.GameObjects.Enemies;

public class RegularEnemy : EnemyBase
{
    private static readonly char[] Sprite = {'<', 'O', '>'};

    public new static readonly int Width = 4;
    public new static readonly int Height = 2;
    public new static int Score => 100;
    
    public RegularEnemy(int posX, int posY) : base(posX, posY)
    {
        Health = 2;
        Color = ConsoleColor.Yellow;
        
        ProjectileSymbol = '|';
        ProjectileColor = ConsoleColor.Red;
        Speed = 0.5f;
        
        AttackDelay = 10;
        AttackCd = AttackDelay;
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

    public override void Remove()
    {
        ObjectManager.MarkForRemoval(this);
        IsActive = false;
    }
}