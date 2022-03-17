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

    private const ConsoleColor Color = ConsoleColor.DarkGreen;
    private const ConsoleColor ProjectileColor = ConsoleColor.Cyan;
    private const int AttackDelay = 5;
    
    // public Position Pos { get; }
    // public int Health { get; }

    public Position Pos;
    public int Health = 5;
        
    private int _attackCD;

    public Player(int startX = 30, int startY = 20)
    {
        Pos.X = startX;
        Pos.Y = startY;
    }


}