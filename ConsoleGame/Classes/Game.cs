using System;
using System.Diagnostics;
using ConsoleGame.Structs;

namespace ConsoleGame.Classes;

public static class Game
{
    public const int GameScreenWidth = 100;
    public const int GameScreenHeight = 30;
    
    private const int TickSpeed = 200;

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
            
            // MoveProjectiles();
            ObjectManager.Update();
            ObjectManager.Clear();
            Display.Update();
            
            stopwatch.Stop();
            var timeToSleep = Math.Max(TickSpeed - (int) stopwatch.ElapsedMilliseconds, 0);
            stopwatch.Reset();

            Thread.Sleep(timeToSleep);
        }
    }

    // private static void MoveProjectiles()
    // {
    //     foreach (var projectile in Projectiles)
    //     {
    //         projectile.Move();
    //         projectile.Draw();
    //     }
    // }

    // public static void AddProjectile(ProjectileInfo info)
    // {
    //     Projectiles.Add(new Projectile(info));
    // }
}
