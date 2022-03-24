namespace ConsoleGame.Classes.GameObjects;

public class Obstacle : GameObject
{
    private const char PlayerDamaged = (char) 223;
    private const char EnemyDamaged = (char) 220;

    public Obstacle(int posX, int posY, int health = 2) : base(posX, posY)
    {
        Pos.X = posX;
        Pos.Y = posY;
        Health = health;

        Symbol = (char) 219; // '█'
        Color = ConsoleColor.White;
    }

    public override bool HitBox(ref Projectile projectile)
    {
        if (projectile.Position != Pos) return false;

        Symbol = projectile.Hostile ? EnemyDamaged : PlayerDamaged;
        
        Hit(projectile.Damage);
        return true;
    }
}
