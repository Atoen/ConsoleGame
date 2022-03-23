using ConsoleGame.Structs;

namespace ConsoleGame.Classes;

public static class GameInterface
{
    private static Position _healthPos = (20, 2);
    private static Position _scorePos = (80, 2);
    
    public static void Draw()
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.SetCursorPosition(0, Game.GameScreenHeight);
        
        for (var i = 0; i < Game.GameScreenWidth * Game.InterfaceHeight; i++)
        {
            Console.Write(' ');
        }
        
        Console.SetCursorPosition(_healthPos.X - 10, _healthPos.Y + Game.GameScreenHeight);
        Console.Write($"Health: {Game.PLayerHealth}");
        
        Console.SetCursorPosition(_scorePos.X - 8, _scorePos.Y + Game.GameScreenHeight);
        Console.Write("Score: 0");
        
        Console.ResetColor();
        Console.SetCursorPosition(Game.GameScreenWidth - 1, Game.GameScreenHeight - 1);
        Console.Write(' ');

    }

    public static void Update()
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;

        Console.SetCursorPosition(_healthPos.X, _healthPos.Y);
        Console.Write(Game.PLayerHealth);
        
        Console.SetCursorPosition(_scorePos.X, _scorePos.Y);
        Console.Write(Game.Score);
    }
}