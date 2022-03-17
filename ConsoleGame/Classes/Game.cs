using System;

namespace ConsoleGame.Classes;

public class Game
{
    public Game()
    {
        
    }

    #pragma warning disable CA1416
    public void Start()
    {
        Console.WindowWidth = 80;
        Console.WindowHeight = 30;
        
        Console.SetBufferSize(80, 30);
    }
}
