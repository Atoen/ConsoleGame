using System.Diagnostics;
using ConsoleGame.Classes.GameObjects;
using ConsoleGame.Classes.GameObjects.Enemies;

namespace ConsoleGame.Classes;

public static class Game
{
    public const int GameScreenWidth = 100;
    public const int GameScreenHeight = 25;
    public const int InterfaceHeight = 5;
    private const int TickSpeed = 50;
    
    private static bool _isRunning = true;
    private static bool _victory;
    private static int _score;
    public static event EventHandler? InterfaceEvent;
    
    public static int Score
    {
        get => _score;

        set
        {
            _score = value;
            InterfaceEvent?.Invoke(null, EventArgs.Empty);
        }
    }

#pragma warning disable CA1416
    public static void Start()
    {
        Console.Title = "Game";
        Console.WindowWidth = GameScreenWidth;
        Console.WindowHeight = GameScreenHeight + InterfaceHeight;
        Console.SetBufferSize(GameScreenWidth, GameScreenHeight + InterfaceHeight);
        Console.CursorVisible = false;

        Input.QuitEvent += (_, _) => _isRunning = false;
        ObjectManager.GameOverEvent += (_, args) =>
        {
            _victory = args.Won;
            _isRunning = false;
        };

        // for (var i = 0; i < byte.MaxValue; i++)
        //     Display.Print(i % GameScreenWidth, i / GameScreenWidth, (char) i, ConsoleColor.White);
        // Display.Update();

        DrawSplashScreen();
        
        GameInterface.Draw();

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

            ObjectManager.Update();
            ObjectManager.CleanUp();

            Display.Update();
            
            stopwatch.Stop();
            var timeToSleep = Math.Max(TickSpeed - (int) stopwatch.ElapsedMilliseconds, 0);
            stopwatch.Reset();
            
            Thread.Sleep(timeToSleep);
        }
        
        Console.ResetColor();
        Console.Clear();
        
        var message = _victory ? "You won!" : "Game over!";
        
        Console.WriteLine($"{message}\nYou scored {Score} points!");
        Console.WriteLine($"Press any key to exit...");
        Console.ReadKey();
    }

    private static void PrepareField()
    {
        for (var i = 20; i < 80; i++)
        {
            if (i == 40) i += 20;

            ObjectManager.Add(new Obstacle(i, 18));
        }

        ObjectManager.Add<TankEnemy>(new EnemyGroup(5, 1, 50, 1, EnemyDirection.Left));
        
        ObjectManager.Add<RegularEnemy>(new EnemyGroup(8, 2, 30, 6));
        
        ObjectManager.Add<FastEnemy>(new EnemyGroup(2, 1, 70, 12, EnemyDirection.Left));
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
