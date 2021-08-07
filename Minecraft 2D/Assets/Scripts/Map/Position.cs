public struct Position 
{
    public int x;
    public int y;

    public Position(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static bool operator ==(Position a, Position b)
    {
        if(a.x == b.x && a.y == b.y)
        {
            return true;
        }
        return false;
    }

    public static bool operator !=(Position a, Position b)
    {
        if (a.x != b.x || a.y != b.y)
        {
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return $"Position x: {x} y: {y}";
    }
}
