public class Trap : AGameElement
{
    public override string SPRITE { get => "*"; }

    public Trap(Map chart, Position position) : base(chart)
    {
        Position = position;
    }
}
