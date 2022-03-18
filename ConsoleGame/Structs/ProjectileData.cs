namespace ConsoleGame.Structs;

public struct ProjectileData
{
    public readonly int PosX;
    public readonly int PosY;
    public readonly float Speed;
    public readonly bool Hostile;
    public readonly int Damage;
    public readonly char Symbol;
    public readonly ConsoleColor Color;
    public readonly ProjectileDirection Direction;

    public ProjectileData()
    {
        PosX = 0;
        PosY = 0;
        Speed = 1;
        Hostile = true;
        Damage = 1;
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