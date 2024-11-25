using CowHexgrid;

namespace CowHexgridTests;

public class HexgridTests
{
    [Fact]
    public void GetNumColumns()
    {
        const int numCols = 6;
        var hexgrid = Given.ANewHexgridWithSize(numCols, 5);

        var actual = hexgrid.GetNumColumns();

        Assert.Equal(numCols, actual);
    }

    [Fact]
    public void GetNumRows()
    {
        const int numRows = 5;
        var hexgrid = Given.ANewHexgridWithSize(6, numRows);

        var actual = hexgrid.GetNumRows();

        Assert.Equal(numRows, actual);
    }

    [Fact]
    public void EnumerateCoordsByColumn()
    {
        var hexgrid = Given.ANewHexgridWithSize(3, 2);

        var actual = hexgrid.EnumerateCoordsByColumn();

        IEnumerable<Coords> expected = [
            new Coords(0, 0),
            new Coords(0, 1),
            new Coords(1, 0),
            new Coords(1, 1),
            new Coords(2, 0),
            new Coords(2, 1),
        ];
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EnumerateCoordsByRow()
    {
        var hexgrid = Given.ANewHexgridWithSize(3, 2);

        var actual = hexgrid.EnumerateCoordsByRow();

        IEnumerable<Coords> expected = [
            new Coords(0, 0),
            new Coords(1, 0),
            new Coords(2, 0),
            new Coords(0, 1),
            new Coords(1, 1),
            new Coords(2, 1),
        ];
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void GetTileAt()
    {
        var hexgrid = Given.ANewHexgrid();

        var result = When.GetTileAt(hexgrid, 1, 0);

        Then.TileIs(result, new Coords(1, 0));
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(10, 0)]
    [InlineData(0, -1)]
    [InlineData(0, 10)]
    public void GetTileAtInvalidLocation(int q, int r)
    {
        var hexgrid = Given.ANewHexgrid();

        var result = When.GetTileAt(hexgrid, q, r);

        Then.TileIsNull(result);
    }

    [Theory]
    [MemberData(nameof(GetNorthTileTestCases))]
    public void GetNorthTile(int q, int r, Coords expected)
    {
        var hexgrid = Given.ANewHexgrid();

        var result = When.GetNorthTile(hexgrid, q, r);

        Then.TileIs(result, expected);
    }

    public static IEnumerable<object?[]> GetNorthTileTestCases()
    {
        return
        [
            [0, 0, null],
            [0, 1, new Coords(0, 0)],
            [1, 0, null],
            [1, 1, new Coords(1, 0)],
            [2, 0, null],
            [2, 1, new Coords(2, 0)],
        ];
    }

    [Theory]
    [MemberData(nameof(GetNorthwestTileTestCases))]
    public void GetNorthwestTile(int q, int r, Coords expected)
    {
        var hexgrid = Given.ANewHexgrid();

        var result = When.GetNorthwestTile(hexgrid, q, r);

        Then.TileIs(result, expected);
    }

    public static IEnumerable<object?[]> GetNorthwestTileTestCases()
    {
        return
        [
            [0, 0, null],
            [0, 1, null],
            [1, 0, new Coords(0, 0)],
            [1, 1, new Coords(0, 1)],
            [2, 0, null],
            [2, 1, new Coords(1, 0)],
        ];
    }

    [Theory]
    [MemberData(nameof(GetNortheastTileTestCases))]
    public void GetNortheastTile(int q, int r, Coords expected)
    {
        var hexgrid = Given.ANewHexgrid();

        var result = When.GetNortheastTile(hexgrid, q, r);

        Then.TileIs(result, expected);
    }

    public static IEnumerable<object?[]> GetNortheastTileTestCases()
    {
        return
        [
            [0, 0, null],
            [0, 1, new Coords(1, 0)],
            [1, 0, new Coords(2, 0)],
            [1, 1, new Coords(2, 1)],
            [2, 0, null],
            [2, 1, null],
        ];
    }

    [Theory]
    [MemberData(nameof(GetSouthTileTestCases))]
    public void GetSouthTile(int q, int r, Coords expected)
    {
        var hexgrid = Given.ANewHexgrid();

        var result = When.GetSouthTile(hexgrid, q, r);

        Then.TileIs(result, expected);
    }

    public static IEnumerable<object?[]> GetSouthTileTestCases()
    {
        return
        [
            [0, 0, new Coords(0, 1)],
            [0, 1, null],
            [1, 0, new Coords(1, 1)],
            [1, 1, null],
            [2, 0, new Coords(2, 1)],
            [2, 1, null],
        ];
    }

    [Theory]
    [MemberData(nameof(GetSouthwestTileTestCases))]
    public void GetSouthwestTile(int q, int r, Coords expected)
    {
        var hexgrid = Given.ANewHexgrid();

        var result = When.GetSouthwestTile(hexgrid, q, r);

        Then.TileIs(result, expected);
    }

    public static IEnumerable<object?[]> GetSouthwestTileTestCases()
    {
        return
        [
            [0, 0, null],
            [0, 1, null],
            [1, 0, new Coords(0, 1)],
            [1, 1, null],
            [2, 0, new Coords(1, 0)],
            [2, 1, new Coords(1, 1)],
        ];
    }

    [Theory]
    [MemberData(nameof(GetSoutheastTileTestCases))]
    public void GetSoutheastTile(int q, int r, Coords expected)
    {
        var hexgrid = Given.ANewHexgrid();

        var result = When.GetSoutheastTile(hexgrid, q, r);

        Then.TileIs(result, expected);
    }

    public static IEnumerable<object?[]> GetSoutheastTileTestCases()
    {
        return
        [
            [0, 0, new Coords(1, 0)],
            [0, 1, new Coords(1, 1)],
            [1, 0, new Coords(2, 1)],
            [1, 1, null],
            [2, 0, null],
            [2, 1, null],
        ];
    }

    [Theory]
    [InlineData(1, 0, 0, 0, true)]
    [InlineData(1, 0, 0, 1, true)]
    [InlineData(1, 0, 1, 1, true)]
    [InlineData(1, 0, 2, 0, true)]
    [InlineData(1, 0, 2, 1, true)]
    [InlineData(1, 1, 1, 0, true)]
    [InlineData(0, 0, 0, 0, false)]
    [InlineData(0, 0, 2, 0, false)]
    [InlineData(1, 1, 0, 0, false)]
    [InlineData(-1, 0, 0, 0, false)]
    public void TestAreNeighbors(int q1, int r1, int q2, int r2, bool expected)
    {
        var a = new Coords(q1, r1);
        var b = new Coords(q2, r2);
        var hexgrid = Given.ANewHexgrid();

        var result = hexgrid.AreNeighbors(a, b);

        Assert.Equal(expected, result);
    }
}

static class Given
{
    const int Q = 3;
    const int R = 2;
    internal static Hexgrid<Coords> ANewHexgrid()
    {
        return ANewHexgridWithSize(Q, R);
    }

    internal static Hexgrid<Coords> ANewHexgridWithSize(int numCols, int numRows)
    {
        var grid = new Hexgrid<Coords>(numCols, numRows);
        for (int q = 0; q < numCols; q++)
        {
            for (int r = 0; r < numRows; r++)
            {
                grid.SetTileAt(q, r, new Coords(q, r));
            }
        }

        return grid;
    }
}

static class When
{
    internal static Coords? GetNorthTile(Hexgrid<Coords> hexgrid, int q, int r)
    {
        return hexgrid.GetNorthTile(q, r);
    }

    internal static Coords? GetNorthwestTile(Hexgrid<Coords> hexgrid, int q, int r)
    {
        return hexgrid.GetNorthwestTile(q, r);
    }

    internal static Coords? GetNortheastTile(Hexgrid<Coords> hexgrid, int q, int r)
    {
        return hexgrid.GetNortheastTile(q, r);
    }

    internal static Coords? GetSouthTile(Hexgrid<Coords> hexgrid, int q, int r)
    {
        return hexgrid.GetSouthTile(q, r);
    }
    
    internal static Coords? GetSouthwestTile(Hexgrid<Coords> hexgrid, int q, int r)
    {
        return hexgrid.GetSouthwestTile(q, r);
    }

    internal static Coords? GetSoutheastTile(Hexgrid<Coords> hexgrid, int q, int r)
    {
        return hexgrid.GetSoutheastTile(q, r);
    }

    internal static Coords? GetTileAt(Hexgrid<Coords> hexgrid, int q, int r)
    {
        return hexgrid.GetTileAt(q, r);
    }
}

static class Then
{
    internal static void TileIs(Coords? actual, Coords? expected)
    {
        if (expected is null)
        {
            Assert.Null(actual);
        }
        else
        {
            Assert.Equal(expected, actual);
        }
    }

    internal static void TileIsNull(Coords? actual)
    {
        Assert.Null(actual);
    }
}