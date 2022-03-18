namespace ConsoleGame.Classes;

public static class Display
{
    private static readonly char[,] ScreenBuffer = new char[Game.GameScreenWidth, Game.GameScreenHeight];
    private static readonly char[,] ScreenBufferOld = new char[Game.GameScreenWidth, Game.GameScreenHeight];
    private static readonly ConsoleColor[,] ColorBuffer = new ConsoleColor[Game.GameScreenWidth, Game.GameScreenHeight];
    
    private static bool _modified;

    public static void Update()
    {
        if (!_modified) return;

        for (var i = 0; i < Game.GameScreenWidth; i++)
        {
            for (var j = 0; j < Game.GameScreenHeight; j++)
            {
                if (ScreenBuffer[i, j] == ScreenBufferOld[i, j]) continue;
                
                Console.SetCursorPosition(i, j);
                Console.ForegroundColor = ColorBuffer[i, j];
                Console.WriteLine(ScreenBuffer[i, j]);

                ScreenBufferOld[i, j] = ScreenBuffer[i, j];
            }
        }

        _modified = false;
    }

    public static void Print(int posX, int posY, char symbol)
    {
        if (ScreenBuffer[posX, posY] == symbol) return;

        ScreenBuffer[posX, posY] = symbol;
        _modified = true;
    }

    public static void Print(int posX, int posY, char symbol, ConsoleColor color)
    {
        if (ScreenBuffer[posX, posY] == symbol) return;

        ScreenBuffer[posX, posY] = symbol;
        ColorBuffer[posX, posY] = color;
        _modified = true;
    }
}