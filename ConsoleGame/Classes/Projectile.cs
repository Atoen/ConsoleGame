using ConsoleGame.Structs;

namespace ConsoleGame.Classes;

public class Projectile
{
    private readonly char _symbol;
    private readonly ConsoleColor _color;
    private readonly float _speed;
    public readonly int damage;
    
    
    public Position pos;
    
    public Projectile()
    {
        
    }
}