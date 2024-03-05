using System.Text;

FileStream debug = File.OpenWrite("./log.log");

Player player = new() {
    Position = new Position(Console.WindowWidth / 2, Console.WindowHeight / 2)
};
bool hasEnded = false;

Bootstrap();

void Bootstrap()
{
    Console.Clear();
    SetupTraps();
    Draw(player.Position, player.SPRITE);

    while (!hasEnded)
    {
        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.UpArrow:
                Move(new Position(player.Position.x, player.Position.y - 1));
                break;
            case ConsoleKey.DownArrow:
                Move(new Position(player.Position.x, player.Position.y + 1));
                break;
            case ConsoleKey.LeftArrow:
                Move(new Position(player.Position.x - 1, player.Position.y));
                break;
            case ConsoleKey.RightArrow:
                Move(new Position(player.Position.x + 1, player.Position.y));
                break;
            case ConsoleKey.Escape:
                End();
                return;
            case ConsoleKey.Q:
                End();
                return;
        }
        Clear(player.Position, player.SPRITE);
        Draw(player.Position, player.SPRITE);
    }
}

void Move(Position newPosition)
{
    if (newPosition.x + player.SPRITE.Length > Console.WindowWidth)
        newPosition.x = Console.WindowWidth - player.SPRITE.Length;
    if (newPosition.x < 0)
        newPosition.x = 0;
    if (newPosition.y < 0)
        newPosition.y = 0;
    if (newPosition.y > Console.WindowHeight)
        newPosition.y = Console.WindowHeight - 1;
    player.Position = newPosition;
}

void Clear(Position to, string target)
{
    Console.Write(new string(' ', target.Length));
}

void Draw(Position to, string target)
{
    Console.SetCursorPosition(to.x, to.y);
    Console.Write(target);
    Console.SetCursorPosition(to.x, to.y);
}

void End()
{
    Console.Clear();
    hasEnded = true;
    debug.Close();
}

void SetupTraps()
{
    Random r = new();

    for (int i = 0; i < 15; i++)
    {
        Trap trap = new Trap {
            Position = new Position(
                r.Next(Console.WindowWidth),
                r.Next(Console.WindowHeight)
            )
        };

        Draw(trap.Position, trap.SPRITE);
    }

}

void Debug(string log)
{
    if (debug.CanWrite)
    {
        byte[] info = new UTF8Encoding(true).GetBytes(log);
        debug.Write(info, 0, info.Length);
    }
}

public record struct Position(int x, int y)
{
    public int x = x;
    public int y = y;
};

public interface IGameElement
{
    public string SPRITE { get; }
    public Position Position { get; set; }
}

public record struct Player : IGameElement
{
    public readonly string SPRITE { get => "-_-"; }
    public Position Position { get; set; }
}

public record struct Trap : IGameElement
{
    public readonly string SPRITE { get => "*"; }
    public Position Position { get; set; }
}
