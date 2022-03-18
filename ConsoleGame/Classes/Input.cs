namespace ConsoleGame.Classes;

public static class Input
{
    private const ConsoleKey QuitKey = ConsoleKey.Escape;
    
    private static Actions _action;
    public static Actions Get
    {
        get
        {
            var action = _action;
            _action = Actions.None;

            return action;
        }
    }

    public static void GetInput()
    {
        ConsoleKeyInfo input;

        do
        {
            input = Console.ReadKey(true);

            _action = input.Key switch
            {
                ConsoleKey.UpArrow => Actions.Up,
                ConsoleKey.DownArrow => Actions.Down,
                ConsoleKey.LeftArrow => Actions.Left,
                ConsoleKey.RightArrow => Actions.Right,
                ConsoleKey.Spacebar => Actions.Shoot,
                _ => _action
            };
        } while (input.Key != QuitKey);
    }
}

public enum Actions
{
    None,
    Up,
    Down,
    Left,
    Right,
    Shoot
}