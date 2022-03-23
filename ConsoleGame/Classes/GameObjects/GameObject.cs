using ConsoleGame.Interfaces;
using ConsoleGame.Structs;

namespace ConsoleGame.Classes.GameObjects;

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

    public virtual void Draw()
    {
        Display.Print(Pos.X, Pos.Y, Symbol, Color);
    }

    public virtual bool HitBox(Projectile projectile)
    {
        if (projectile.Pos != Pos) return false;
        
        Hit(projectile.Damage);
        return true;
    }

    protected void Hit(int damage)
    {
        Health -= damage;

        if (Health < 1)
        {
            Remove();
        }
    }

    public virtual void Clear()
    {
        Display.ClearAt(Pos.X, Pos.Y);
    }

    public void Remove()
    {
        ObjectManager.MarkForRemoval(this);
    }
}
