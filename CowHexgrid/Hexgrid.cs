
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

    public T? GetNorthTile(int q, int r)
    {
        return GetTileAt(q, r-1);
    }
    
    public T? GetNorthwestTile(int q, int r)
    {
        if (IsOdd(q))
        {
            return GetTileAt(q - 1, r);
        }
        else
        {
            return GetTileAt(q - 1, r - 1);
        }
    }

    public T? GetNortheastTile(int q, int r)
    {
        if (IsOdd(q))
        {
            return GetTileAt(q + 1, r);
        }
        else
        {
            return GetTileAt(q + 1, r - 1);
        }
    }

    public T? GetSouthTile(int q, int r)
    {
        return GetTileAt(q, r+1);
    }

    public T? GetSouthwestTile(int q, int r)
    {
        if (IsOdd(q))
        {
            return GetTileAt(q - 1, r + 1);
        }
        else
        {
            return GetTileAt(q - 1, r);
        }
    }

    public T? GetSoutheastTile(int q, int r)
    {
        if (IsOdd(q))
        {
            return GetTileAt(q + 1, r + 1);
        }
        else
        {
            return GetTileAt(q + 1, r);
        }
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


