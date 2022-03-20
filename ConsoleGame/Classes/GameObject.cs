using ConsoleGame.Interfaces;
using ConsoleGame.Structs;

namespace ConsoleGame.Classes;

public class GameObject : IRemovable
{
    protected char Symbol;
    protected ConsoleColor Color;
    protected Position Pos;
    protected int Health;

    protected GameObject(int posX, int posY)
    {
        Pos.X = posX;
        Pos.Y = posY;
    }

    public void Draw()
    {
        Display.Print(Pos.X, Pos.Y, Symbol, Color);
    }

    public bool HitBox(Projectile projectile)
    {
        if (projectile.Pos != Pos) return false;
        
        Hit(projectile.Damage);
        return true;
    }

    private void Hit(int damage)
    {
        Health -= damage;

        if (Health < 1)
        {
            Remove();
        }
    }

    public void Remove()
    {
        ObjectManager.MarkForRemoval(this);
    }

    public void Clear()
    {
        Display.ClearAt(Pos.X, Pos.Y);
    }
}