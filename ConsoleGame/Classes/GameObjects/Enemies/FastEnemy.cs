namespace ConsoleGame.Classes.GameObjects.Enemies;

public class FastEnemy : EnemyBase
{
    private static readonly char[] Sprite = {'-', 'Y', '-'};
    
    public new int Score = 100;
    
    public FastEnemy(int posX, int posY) : base(posX, posY)
    {
        Health = 1;
        Color = ConsoleColor.Green;
        
        ProjectileSymbol = 'v';
        ProjectileColor = ConsoleColor.Blue;
        Speed = 1f;

        Width = 4;
        Height = 2;
        
        AttackDelay = 5;
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