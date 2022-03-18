﻿using ConsoleGame.Structs;

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

    public void PerformAction(Actions action)
    {
        if (action == Actions.None) return;
        
        Clear();
        
        switch (action)
        {
            case Actions.Up when Pos.Y > 1:
                Pos.Y--;
                break;
            
            case Actions.Down when Pos.Y < Game.GameScreenHeight - 4:
                Pos.Y++;
                break;
            
            case Actions.Left when Pos.X > 1:
                Pos.X--;
                break;
            
            case Actions.Right when Pos.X < Game.GameScreenWidth - 2:
                Pos.X++;
                break;
            
            case Actions.Shoot:
                Shoot();
                break;
        }
    }

    private void Shoot()
    {
        
    }

    public void Draw()
    {
        for (var i = 0; i < Sprite.GetLength(0); i++)
        {
            Display.Print(Pos.X + i - 1, Pos.Y - 1, Sprite[0, i], Color);
            Display.Print(Pos.X + i - 1, Pos.Y, Sprite[1, i], Color);
            Display.Print(Pos.X + i - 1, Pos.Y + 1, Sprite[2, i], Color);
        }
    }

    private void Clear()
    {
        for (var i = 0; i < Sprite.GetLength(0); i++)
        {
            Display.ClearAt(Pos.X + i - 1, Pos.Y + 1);
            Display.ClearAt(Pos.X + i - 1, Pos.Y);
            Display.ClearAt(Pos.X + i - 1, Pos.Y - 1);
            
        }
    }
}