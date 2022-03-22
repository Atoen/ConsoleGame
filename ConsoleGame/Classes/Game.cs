using System.Diagnostics;

namespace ConsoleGame.Classes;

public static class Game
{
    public const int GameScreenWidth = 100;
    public const int GameScreenHeight = 30;
    private const int TickSpeed = 50;

    private static readonly Player Player = new();
    private static bool _isRunning = true;

#pragma warning disable CA1416
    public static void Start()
    {
        Console.Title = "Game";
        Console.WindowWidth = GameScreenWidth;
        Console.WindowHeight = GameScreenHeight;
        Console.SetBufferSize(GameScreenWidth, GameScreenHeight);
        Console.CursorVisible = false;

        Input.QuitEvent += (_, _) => _isRunning = false;

        DrawSplashScreen();
        
        PrepareField();
        
        var inputThread = new Thread(Input.GetInput);
        inputThread.Start();
        MainLoop();
    }

    private static void MainLoop()
    {
        Stopwatch stopwatch = new();

        while (_isRunning)
        {
            stopwatch.Start();

            Player.PerformAction(Input.Get);
            
            ObjectManager.Update();
            ObjectManager.CleanUp();
            
            Player.Draw();
            
            Display.Update();
            
            stopwatch.Stop();
            var timeToSleep = Math.Max(TickSpeed - (int) stopwatch.ElapsedMilliseconds, 0);
            stopwatch.Reset();

            Thread.Sleep(timeToSleep);
        }
        
        Console.Clear();
    }

    private static void PrepareField()
    {
        for (var i = 0; i < 10; i++)
        {
            ObjectManager.Add(new Obstacle(10 + i, 15));
        }

        ObjectManager.Add(new EnemyGroup(5, 3, EnemyDirection.Left)
        {
            StartX = 5,
            StartY = 5,
        });
    }

    private static void DrawSplashScreen()
    {
        string[] splashScreen =
        {
            @"                  ████████╗██╗  ██╗███████╗     ██████╗  █████╗ ███╗   ███╗███████╗",
            @"                  ╚══██╔══╝██║  ██║██╔════╝    ██╔════╝ ██╔══██╗████╗ ████║██╔════╝",
            @"                     ██║   ███████║█████╗      ██║  ███╗███████║██╔████╔██║█████╗  ",
            @"                     ██║   ██╔══██║██╔══╝      ██║   ██║██╔══██║██║╚██╔╝██║██╔══╝  ",
            @"                     ██║   ██║  ██║███████╗    ╚██████╔╝██║  ██║██║ ╚═╝ ██║███████╗",
            @"                     ╚═╝   ╚═╝  ╚═╝╚══════╝     ╚═════╝ ╚═╝  ╚═╝╚═╝     ╚═╝╚══════╝"

        };

        Console.SetCursorPosition(0, 10);
        Console.ForegroundColor = ConsoleColor.Yellow;
        
        foreach (var line in splashScreen)
        {
            Console.WriteLine(line);
        }
        
        Console.ResetColor();
        
        Console.SetCursorPosition(40, 20);
        Console.Write("Press any key");
        
        Console.ReadKey();
        Console.Clear();
    }
}
