public abstract class AGameElement(Map chart)
{
    public abstract string SPRITE { get; }
    public Position Position { get; set; }
    protected Map map { get; set; } = chart;

    public void SetInstanceOnMap()
    {
        map.SetElement(Position, SPRITE);
    }

    public bool isMapInstance(string? s)
    {
        return SPRITE == s;
    }

    public bool isMapInstance(char? c)
    {
        if (SPRITE.Length > 1) return false;
        return SPRITE[0] == c;
    }
}
