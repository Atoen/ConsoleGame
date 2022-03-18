using System;
using System.Diagnostics;

namespace ConsoleGame.Classes;

public static class Game
{
    public const int GameScreenWidth = 100;
    public const int GameScreenHeight = 30;
    
    private const int TickSpeed = 50;

    private static readonly Player Player = new();

#pragma warning disable CA1416
    public static void Start()
    {
        Console.WindowWidth = GameScreenWidth;
        Console.WindowHeight = GameScreenHeight;
        Console.SetBufferSize(GameScreenWidth, GameScreenHeight);
        Console.CursorVisible = false;

        var inputThread = new Thread(Input.GetInput);
        inputThread.Start();
        MainLoop();
    }

    private static void MainLoop()
    {
        Stopwatch stopwatch = new();

        while (true)
        {
            stopwatch.Start();

            Player.PerformAction(Input.Get);
            Player.Draw();
            
            Display.Update();
            
            stopwatch.Stop();
            var timeToSleep = Math.Max(TickSpeed - (int) stopwatch.ElapsedMilliseconds, 0);
            stopwatch.Reset();
            
            Thread.Sleep(timeToSleep);
        }
    }
}
