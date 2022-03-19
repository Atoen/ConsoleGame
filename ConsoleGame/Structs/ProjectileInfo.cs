using System.Collections;

namespace ConsoleGame.Structs;

public struct ProjectileInfo
{
    public int PosX;
    public int PosY;
    public float Speed = 1;
    public bool Hostile = true;
    public int Damage = 1;
    public char Symbol;
    public ConsoleColor Color;
    public ProjectileDirection Direction;

    public ProjectileInfo()
    {
        PosX = 0;
        PosY = 0;
        Symbol = '|';
        Color = ConsoleColor.White;
        Direction = ProjectileDirection.Down;
    }

    public ProjectileInfo(int posX, int posY)
    {
        PosX = posX;
        PosY = posY;
        Symbol = '|';
        Color = ConsoleColor.White;
        Direction = ProjectileDirection.Down;
    }
}

public enum ProjectileDirection
{
    Up,
    Down,
    Left,
    Right
}