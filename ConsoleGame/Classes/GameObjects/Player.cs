using ConsoleGame.Classes.GameObjects.Projectiles;
using ConsoleGame.Structs;

namespace ConsoleGame.Classes.GameObjects;

public class Player : GameObject
{
    private static readonly char[,] Sprite =
    {
        {'_', (char) 30, '_'},
        {'/', (char) 219, '\\'},
        {(char) 19, ' ', (char) 19}
    };
    
    private const char ProjectileSymbol = '|';
    private const ConsoleColor ProjectileColor = ConsoleColor.Cyan;
    private const int AttackDelay = 5;

    private int _attackCd;

    public static event EventHandler? HitEvent; 
    public int CurrentHealth => Health;
    public Position Position => Pos;

    public Player(int posX = 50, int posY = 22) : base(posX, posY)
    {
        Color = ConsoleColor.Green;
        Health = 10;
    }
    
    public void PerformAction(Actions action)
    {
        if (_attackCd > 0) _attackCd--;

        if (action == Actions.None) return;

        Clear();

        switch (action)
        {
            case Actions.Up when Pos.Y > 1:
                Pos.Y--;
                break;

            case Actions.Down when Pos.Y < Game.GameScreenHeight - 2:
                Pos.Y++;
                break;

            case Actions.Left when Pos.X > 1:
                Pos.X--;
                break;

            case Actions.Right when Pos.X < Game.GameScreenWidth - 2:
                Pos.X++;
                break;

            case Actions.Shoot:
                Shoot();
                break;
        }
    }
    
    private void Shoot()
    {
        if (_attackCd > 0) return;

        _attackCd = AttackDelay;

        var info = new ProjectileInfo(Pos.X, Pos.Y - 1)
        {
            Symbol = ProjectileSymbol,
            Color = ProjectileColor,
            Hostile = false,
            Direction = ProjectileDirection.Up
        };

        ObjectManager.Add(new Projectile(info));
    }

    public override void Draw()
    {
        for (var i = 0; i < Sprite.GetLength(0); i++)
        {
            Display.Print(Pos.X + i - 1, Pos.Y - 1, Sprite[0, i], Color);
            Display.Print(Pos.X + i - 1, Pos.Y, Sprite[1, i], Color);
            Display.Print(Pos.X + i - 1, Pos.Y + 1, Sprite[2, i], Color);
        }
    }

    public override bool HitBox(ref Projectile projectile)
    {
        if (!(MathF.Abs(Pos.Y - projectile.Position.Y) <= 1) ||
            !(MathF.Abs(Pos.X - projectile.Position.X) <= 1)) return false;
        
        Hit(projectile.Damage);
        HitEvent?.Invoke(null, EventArgs.Empty);
        return true;

    }

    public override void Clear()
    {
        for (var i = 0; i < Sprite.GetLength(0); i++)
        {
            Display.ClearAt(Pos.X + i - 1, Pos.Y + 1);
            Display.ClearAt(Pos.X + i - 1, Pos.Y);
            Display.ClearAt(Pos.X + i - 1, Pos.Y - 1);
        }
    }
}