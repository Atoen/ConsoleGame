﻿using System.Diagnostics;
using ConsoleGame.Classes.GameObjects;

namespace ConsoleGame.Classes;

public static class Game
{
    public const int GameScreenWidth = 100;
    public const int GameScreenHeight = 25;
    public const int InterfaceHeight = 5;
    private const int TickSpeed = 50;

    private static readonly Player Player = new();
    private static bool _isRunning = true;

    public static int PLayerHealth => Player.CurrentHealth;
    public static int Score { get; }

#pragma warning disable CA1416
    public static void Start()
    {
        Console.Title = "Game";
        Console.WindowWidth = GameScreenWidth;
        Console.WindowHeight = GameScreenHeight + InterfaceHeight;
        Console.SetBufferSize(GameScreenWidth, GameScreenHeight + InterfaceHeight);
        Console.CursorVisible = false;

        Input.QuitEvent += (_, _) => _isRunning = false;

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

        ObjectManager.Add(new EnemyGroup(15, 4)
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
