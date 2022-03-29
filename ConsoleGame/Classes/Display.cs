using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace ConsoleGame.Classes;

public static class Display
{
    private static readonly SafeFileHandle SafeFileHandle;
    
    private static readonly CharInfo[] Buffer = new CharInfo[Game.GameScreenWidth * Game.GameScreenHeight];
    private static readonly Coord ScreenSize = new() {X = Game.GameScreenWidth, Y = Game.GameScreenHeight};
    private static readonly Coord SecondCoord = new() {X = 0, Y = 0};

    private static DisplayRect _screenRect = new()
        {Left = 0, Top = 0, Right = Game.GameScreenWidth, Bottom = Game.GameScreenHeight};

    private static bool _modified;

    static Display()
    {
        SafeFileHandle = CreateFile("CONOUT$",
            0x40000000,
            2,
            IntPtr.Zero,
            FileMode.Open,
            0,
            IntPtr.Zero);

        if (SafeFileHandle.IsInvalid) throw new IOException("Console buffer file is invalid");
    }

    public static void Update()
    {
        if (!_modified) return;

        WriteConsoleOutput(SafeFileHandle,
            Buffer,
            ScreenSize,
            SecondCoord,
            ref _screenRect);

        _modified = false;
    }

    public static void Print(int posX, int posY, char symbol, ConsoleColor color)
    {
        if (posX is < 0 or > Game.GameScreenWidth || posY is < 0 or >= Game.GameScreenHeight)
            return;

        var index = posX + Game.GameScreenWidth * posY;
        var symbolByte = (byte) symbol;

        if (Buffer[index].Symbol == symbolByte) return;

        Buffer[index].Symbol = symbolByte;
        Buffer[index].Color = (short) color;

        _modified = true;
    }
    
    public static void ClearAt(int posX, int posY)
    {
        if (posX is < 0 or > Game.GameScreenWidth || posY is < 0 or >= Game.GameScreenHeight)
            return;
        
        var index = posX + Game.GameScreenWidth * posY;
        
        if (Buffer[index].Symbol == 32) return;

        Buffer[index].Symbol = 32; // Spacja ' '
        Buffer[index].Color = 15; // Biały

        _modified = true;
    }
        
    [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern SafeFileHandle CreateFile(string fileName,
        [MarshalAs(UnmanagedType.U4)] uint fileAccess,
        [MarshalAs(UnmanagedType.U4)] uint fileShare,
        IntPtr securityAttributes,
        [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
        [MarshalAs(UnmanagedType.U4)] int flags,
        IntPtr template);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteConsoleOutput(
        SafeFileHandle hConsoleOutput,
        CharInfo[] lpBuffer,
        Coord dwBufferSize,
        Coord dwBufferCoord,
        ref DisplayRect lpWriteRegion);
    
    [StructLayout(LayoutKind.Sequential)]
    private struct Coord
    {
        public short X;
        public short Y;
    }
    
    [StructLayout(LayoutKind.Explicit)]
    private struct CharInfo
    {
        [FieldOffset(0)] public byte Symbol;
        [FieldOffset(2)] public short Color;
    }
    
    [StructLayout(LayoutKind.Sequential)]
    private struct DisplayRect
    {
        public short Left;
        public short Top;
        public short Right;
        public short Bottom;
    }
}