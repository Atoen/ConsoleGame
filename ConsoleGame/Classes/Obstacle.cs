using ConsoleGame.Interfaces;
using ConsoleGame.Structs;

namespace ConsoleGame.Classes;

public class Obstacle : IRemovable
{
    private const char Symbol = '█';
    private const ConsoleColor Color = ConsoleColor.White;

    public Position Pos;
    private int _health;

    public Obstacle(int posX, int posY, int health = 2)
    {
        Pos.X = posX;
        Pos.Y = posY;
        _health = health;
    }

    public void Draw()
    {
        Display.Print(Pos.X, Pos.Y, Symbol, Color);
    }
    
    public void Remove()
    {
        Display.ClearAt(Pos.X, Pos.Y);
    }
}