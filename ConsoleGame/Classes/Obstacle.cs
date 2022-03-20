using ConsoleGame.Interfaces;
using ConsoleGame.Structs;

namespace ConsoleGame.Classes;

public class Obstacle : IRemovable
{
    private const char Symbol = '█';
    private const ConsoleColor Color = ConsoleColor.White;

    private readonly Position _pos;
    private int _health;

    public Obstacle(int posX, int posY, int health = 2)
    {
        _pos.X = posX;
        _pos.Y = posY;
        _health = health;
    }

    public void Draw()
    {
        Display.Print(_pos.X, _pos.Y, Symbol, Color);
    }

    public bool HitBox(Projectile projectile)
    {
        if (projectile.Pos != _pos) return false;

        Hit(projectile.Damage);
        return true;
    }

    private void Hit(int damage)
    {
        _health -= damage;

        if (_health < 1)
        {
            Remove();
        }
    }

    public void Remove()
    {
        ObjectManager.MarkForRemoval(this);
    }

    // Po zflagowaniu do usuniecia przeskodna nadal sie rysuje
    // Usuwanie wykonywanie jest po updacie wszystkich obiektow
    public void Clear()
    {
        Display.ClearAt(_pos.X, _pos.Y);
    }
}