using System.Runtime.CompilerServices;

public class Player : AGameElement
{
    public override string SPRITE { get => "-_-"; }
    private int _Health;

    public int Health
    {
        get => _Health;
        set
        {
            if (value > 0 && value <= 10)
            {
                _Health = value;
            }
        }
    }

    //
    // Events
    //
    public delegate bool BeforeMove(Position p);

    private BeforeMove? onBeforeMove;
    //
    // ------
    //

    public Player(Map chart, int health, Position position, BeforeMove? beforeMove) : base(chart)
    {
        _Health = health;
        Position = position;
        onBeforeMove = beforeMove;
    }

    public void Move(Position newPosition)
    {
        if (newPosition.x + SPRITE.Length > map.Width)
            newPosition.x = map.Width - SPRITE.Length;
        if (newPosition.x < 0)
            newPosition.x = 0;
        if (newPosition.y < 0)
            newPosition.y = 0;
        if (newPosition.y > map.Height)
            newPosition.y = map.Height - 1;

        if (this?.onBeforeMove(newPosition) == true)
            Position = newPosition;
    }


}
