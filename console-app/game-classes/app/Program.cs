using System.Text;

class Game
{
    Map map;
    List<Trap> traps = new(15);
    Player player;

    bool hasEnded;

    public Game(int width, int height)
    {

        map = new(width, height);
        Trap trap = new(map, new Position(
                        0,
                        0
                    ));
        player = new(
            chart: map,
             health: 10,
              position: new Position { x = width / 2, y = height / 2 },
              beforeMove: (p) =>
              {
                  if (trap.isMapInstance(map.GetElement(p)))
                  {
                      player.Health -= 1;
                  }
                  return true;
              }
            );
    }

    public void Bootstrap()
    {
        Console.Clear();
        CreateTraps();
        Draw();
        while (!hasEnded)
        {
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.UpArrow:
                    player.Move(new Position(player.Position.x, player.Position.y - 1));
                    break;
                case ConsoleKey.DownArrow:
                    player.Move(new Position(player.Position.x, player.Position.y + 1));
                    break;
                case ConsoleKey.LeftArrow:
                    player.Move(new Position(player.Position.x - 1, player.Position.y));
                    break;
                case ConsoleKey.RightArrow:
                    player.Move(new Position(player.Position.x + 1, player.Position.y));
                    break;
                case ConsoleKey.Escape:
                    End();
                    return;
                case ConsoleKey.Q:
                    End();
                    return;
            }
            Debugger.Debug($"health: {player.Health}\n");
            Draw();
        }

    }

    void Draw()
    {
        map.Clear();
        player.SetInstanceOnMap();
        traps.ForEach(t => t.SetInstanceOnMap());
        Console.SetCursorPosition(0, 0);
        Console.Write(map.ToString(lineBreak: false));
    }

    void End()
    {
        Console.Clear();
        hasEnded = true;
    }

    void CreateTraps()
    {
        Random r = new();

        for (int i = 0; i < 15; i++)
        {
            Trap trap = new(map, new Position(
                    r.Next(Console.WindowWidth),
                    r.Next(Console.WindowHeight)
                ));
            traps.Add(trap);
        }

    }
}


class Program
{


    static void Main(string[] args)
    {
        Game program = new(Console.WindowWidth, Console.WindowHeight);
        program.Bootstrap();
    }
}
