namespace ConsoleGame.Structs;

public struct ProjectileInfo
{
    public readonly int PosX;
    public readonly int PosY;
    public readonly float Speed = 1;
    public readonly bool Hostile = true;
    public readonly int Damage = 1;
    public readonly char Symbol;
    public readonly ConsoleColor Color;
    public readonly ProjectileDirection Direction;

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