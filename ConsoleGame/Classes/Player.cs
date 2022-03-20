using ConsoleGame.Structs;

namespace ConsoleGame.Classes;

public class Player
{
    private static readonly char[,] Sprite =
    {
        {' ', '^', ' '},
        {'/', '■', '\\'},
        {'|', ' ', '|'}
    };

    private const char ProjectileSymbol = '|';

    private const ConsoleColor Color = ConsoleColor.DarkGreen;
    private const ConsoleColor ProjectileColor = ConsoleColor.Cyan;
    private const int AttackDelay = 5;

    private Position _pos;
    private int _attackCd;
    
    public int Health = 5;

    public Player(int startX = 30, int startY = 20)
    {
        _pos.X = startX;
        _pos.Y = startY;
    }

    public void PerformAction(Actions action)
    {
        if (_attackCd > 0) _attackCd--;

        if (action == Actions.None) return;

        Clear();

        switch (action)
        {
            case Actions.Up when _pos.Y > 1:
                _pos.Y--;
                break;

            case Actions.Down when _pos.Y < Game.GameScreenHeight - 4:
                _pos.Y++;
                break;

            case Actions.Left when _pos.X > 1:
                _pos.X--;
                break;

            case Actions.Right when _pos.X < Game.GameScreenWidth - 2:
                _pos.X++;
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

        var info = new ProjectileInfo(_pos.X, _pos.Y - 1)
        {
            Symbol = ProjectileSymbol,
            Color = ProjectileColor,
            Hostile = false,
            Direction = ProjectileDirection.Up
        };

        ObjectManager.Add(new Projectile(info));
    }

    public void Draw()
    {
        for (var i = 0; i < Sprite.GetLength(0); i++)
        {
            Display.Print(_pos.X + i - 1, _pos.Y - 1, Sprite[0, i], Color);
            Display.Print(_pos.X + i - 1, _pos.Y, Sprite[1, i], Color);
            Display.Print(_pos.X + i - 1, _pos.Y + 1, Sprite[2, i], Color);
        }
    }

    public void HitBox(ProjectileOld projectileOld)
    {
        
    }

    private void Hit(int damage)
    {
        
    }
    
    private void Clear()
    {
        for (var i = 0; i < Sprite.GetLength(0); i++)
        {
            Display.ClearAt(_pos.X + i - 1, _pos.Y + 1);
            Display.ClearAt(_pos.X + i - 1, _pos.Y);
            Display.ClearAt(_pos.X + i - 1, _pos.Y - 1);
        }
    }
}