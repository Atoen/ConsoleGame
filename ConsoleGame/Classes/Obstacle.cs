using System.Diagnostics.SymbolStore;

namespace ConsoleGame.Classes;

public class Obstacle : GameObject
{
    public Obstacle(int posX, int posY, int health = 2) : base(posX, posY)
    {
        Pos.X = posX;
        Pos.Y = posY;
        Health = health;

        Symbol = (char) 219; // █
        Color = ConsoleColor.White;
    }
}
