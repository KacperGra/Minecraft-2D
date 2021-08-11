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
        return a.Equals(b);
    }

    public static bool operator !=(Position a, Position b)
    {
        return !a.Equals(b);
    }

    public static Position operator +(Position a, Position b)
    {
        return new Position(a.x + b.x, a.y + b.y);
    }

    public static Position operator -(Position a, Position b)
    {
        return new Position(a.x - b.x, a.y - b.y);
    }

    public static Position operator *(Position a, Position b)
    {
        return new Position(a.x * b.x, a.y * b.y);
    }

    public static Position operator /(Position a, Position b)
    {
        if(b.x == 0 || b.y == 0)
        {
            throw new System.DivideByZeroException();
        }

        return new Position(a.x / b.x, a.y / b.y);
    }

    public override string ToString()
    {
        return $"Position x: {x} y: {y}";
    }

    public override bool Equals(object obj)
    {
        if((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        else
        {
            Position position = (Position)obj;
            return (x == position.x) && (y == position.y);
        }
    }

    public override int GetHashCode()
    {
        return (x << 2) ^ y;
    }
}
