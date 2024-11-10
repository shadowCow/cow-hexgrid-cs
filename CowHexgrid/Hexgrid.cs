
namespace CowHexgrid;

// odd-q vertical layout
public class Hexgrid<T> where T : class
{
    private int numCols;
    private int numRows;
    private Slot<T>[] grid;

    public Hexgrid(int numCols, int numRows)
    {
        this.numCols = numCols;
        this.numRows = numRows;
        grid = new Slot<T>[numCols*numRows];

        Array.Fill(grid, new Slot<T>.Empty());
    }

    public void SetTileAt(int q, int r, T tile)
    {
        if (IsValidCoord(q, r))
        {
            grid[ToIndex(q, r)] = new Slot<T>.Filled(tile);
        }
    }

    public T? GetTileAt(int q, int r)
    {
        if (!IsValidCoord(q, r))
        {
            return null;
        }

        var slot = grid[ToIndex(q, r)];
        return slot switch
        {
            Slot<T>.Filled f => f.Tile,
            _ => null,
        };
    }

    public T? GetTileAt(Coords coords)
    {
        return GetTileAt(coords.Q, coords.R);
    }

    public T? GetNorthTile(int q, int r)
    {
        return GetTileAt(GetCoordsNorthOf(q, r));
    }
    
    public T? GetNorthwestTile(int q, int r)
    {
        return GetTileAt(GetCoordsNorthwestOf(q, r));
    }

    public T? GetNortheastTile(int q, int r)
    {
        return GetTileAt(GetCoordsNortheastOf(q, r));
    }

    public T? GetSouthTile(int q, int r)
    {
        return GetTileAt(GetCoordsSouthOf(q, r));
    }

    public T? GetSouthwestTile(int q, int r)
    {
        return GetTileAt(GetCoordsSouthwestOf(q, r));
    }

    public T? GetSoutheastTile(int q, int r)
    {
        return GetTileAt(GetCoordsSoutheastOf(q, r));
    }

    public Coords GetCoordsNorthOf(int q, int r)
    {
        return new Coords(q, r-1);
    }

    public Coords GetCoordsNorthwestOf(int q, int r)
    {
        if (IsOdd(q))
        {
            return new Coords(q - 1, r);
        }
        else
        {
            return new Coords(q - 1, r - 1);
        }
    }

    public Coords GetCoordsNortheastOf(int q, int r)
    {
        if (IsOdd(q))
        {
            return new(q + 1, r);
        }
        else
        {
            return new(q + 1, r - 1);
        }
    }

    public Coords GetCoordsSouthOf(int q, int r)
    {
        return new(q, r + 1);
    }

    public Coords GetCoordsSouthwestOf(int q, int r)
    {
        if (IsOdd(q))
        {
            return new(q - 1, r + 1);
        }
        else
        {
            return new(q - 1, r);
        }
    }

    public Coords GetCoordsSoutheastOf(int q, int r)
    {
        if (IsOdd(q))
        {
            return new(q + 1, r + 1);
        }
        else
        {
            return new(q + 1, r);
        }
    }

    public bool AreNeighbors(Coords a, Coords b)
    {
        if (!IsValidCoord(a.Q, a.R) || !IsValidCoord(b.Q, b.R))
        {
            return false;
        }
        if (a == b)
        {
            return false;
        }

        var isNorthOf = b == GetCoordsNorthOf(a.Q, a.R);
        var isNorthwestOf = b == GetCoordsNorthwestOf(a.Q, a.R);
        var isNortheastOf = b == GetCoordsNortheastOf(a.Q, a.R);
        var isSouthOf = b == GetCoordsSouthOf(a.Q, a.R);
        var isSouthwestOf = b == GetCoordsSouthwestOf(a.Q, a.R);
        var isSoutheastOf = b == GetCoordsSoutheastOf(a.Q, a.R);

        return isNorthOf ||
            isNorthwestOf ||
            isNortheastOf ||
            isSouthOf ||
            isSouthwestOf ||
            isSoutheastOf;
    }

    bool IsValidCoord(int q, int r)
    {
        if (q < 0 || q >= numCols)
        {
            return false;
        }
        if (r < 0 || r >= numRows)
        {
            return false;
        }

        return true;
    }

    int ToIndex(int q, int r)
    {
        return (q * numRows) + r;
    }

    static bool IsEven(int i)
    {
        return i % 2 == 0;
    }

    static bool IsOdd(int i)
    {
        return !IsEven(i);
    }
}

public record Coords(int Q, int R);

public abstract record Slot<T>
{
    private Slot() {}

    public sealed record Empty() : Slot<T>;
    public sealed record Filled(T? Tile) : Slot<T>;
}


