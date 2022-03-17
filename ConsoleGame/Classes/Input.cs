﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConsoleGame.Classes;

public class Input
{
    public InputStatus GetInput()
    {
        var status = new InputStatus();
        if (Keyboard.IsKeyDown(Key.Up))
            status.Direction = Direction.UP;
        if (Keyboard.IsKeyDown(Key.Down))
            status.Direction = Direction.DOWN;
        if (Keyboard.IsKeyDown(Key.Left))
            status.Direction = Direction.LEFT;
        if (Keyboard.IsKeyDown(Key.Right))
            status.Direction = Direction.RIGHT;
        return status;
    }
}

public class InputStatus
{
    public Directions Direction { get; set; }
}

public enum Directions
{
    None,
    Up,
    Down,
    Left,
    Right
}