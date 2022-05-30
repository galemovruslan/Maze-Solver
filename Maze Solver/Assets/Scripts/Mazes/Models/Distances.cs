using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distances
{
    private Dictionary<Cell, int> _cells;
    private Cell _root;

    public Distances(Cell root)
    {
        _root = root;
        _cells = new Dictionary<Cell, int>();
        _cells[root] = 0;
    }

    public int this[Cell cell]
    {
        get
        {
            return _cells[cell];
        }
        set
        {
            _cells[cell] = value;
        }
    }

    public bool Contains(Cell cell)
    {
        return _cells.ContainsKey(cell);
    }

    public IEnumerable<Cell> Cells()
    {
        return _cells.Keys;
    }
}
