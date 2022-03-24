using ConsoleGame.Classes.GameObjects;
using ConsoleGame.Structs;

namespace ConsoleGame.Classes;

public static class GameInterface
{
    private static readonly Position HealthPos = (20, 2 + Game.GameScreenHeight);
    private static readonly Position ScorePos = (80, 2 + Game.GameScreenHeight);

    static GameInterface()
    {
        Game.InterfaceEvent += (_, _) => Update();
        Player.HitEvent += (_, _) => Update();
    }

    public static void Draw()
    {
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.SetCursorPosition(0, Game.GameScreenHeight);
        
        // Zapełnianie dołu ekranu
        for (var i = 0; i < Game.InterfaceHeight; i++)
        {
            Console.Write(new string(' ', Game.GameScreenWidth));
        }

        Console.SetCursorPosition(HealthPos.X - 8, HealthPos.Y);
        Console.Write($"Health: {ObjectManager.PlayerHealth}");
        
        Console.SetCursorPosition(ScorePos.X - 7, ScorePos.Y);
        Console.Write("Score: 0");
        
        // Ostatni kwadrat po prawej
        Console.ResetColor();
        Console.SetCursorPosition(Game.GameScreenWidth - 1, Game.GameScreenHeight - 1);
        Console.Write(' ');
        
        Console.BackgroundColor = ConsoleColor.DarkBlue;
    }

    private static void Update()
    {
        Console.SetCursorPosition(HealthPos.X, HealthPos.Y);
        Console.Write(ObjectManager.PlayerHealth);

        Console.SetCursorPosition(ScorePos.X, ScorePos.Y);
        Console.Write(Game.Score);
    }
}