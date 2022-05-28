using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cell
{
    private struct Neighbour 
    {
        public Cell cell;
        public CellSide side;
    }


    public int Row => _row;
    public int Column => _column;

    public Cell North { get => _north; set => _north = value; }
    public Cell East { get => _east; set => _east = value; }
    public Cell South { get => _south; set => _south = value; }
    public Cell West { get => _west; set => _west = value; }

    private int _row;
    private int _column;
    private Cell _north;
    private Cell _east;
    private Cell _south;
    private Cell _west;
    private Dictionary<Cell, bool> _links;

    public Cell(int row, int col)
    {
        _row = row;
        _column = col;
        _links = new Dictionary<Cell, bool>();
    }

    public void Link(Cell cell, bool bidir = true)
    {
        _links[cell] = true;

        if (bidir)
        {
            cell.Link(this, false);
        }
    }

    public void Unlink(Cell cell, bool bidir = true)
    {
        _links.Remove(cell);

        if (bidir)
        {
            cell.Unlink(this, false);
        }
    }

    public List<Cell> Links()
    {
        return _links.Keys.ToList();
    }

    public bool IsLinked(Cell cell)
    {
        return _links.ContainsKey(cell);
    }

    public List<Cell> Neighbours()
    {
        return new List<Cell>() { _north, _east, _south, _west };
    }

    public List<CellSide> LinkDirections()
    {
        if (_links.Count == 0)
        {
            return null;
        }

        var directions = new List<CellSide>();
        var neighbours = Neighbours();

        for (int i = 0; i < neighbours.Count; i++)
        {
            if (neighbours[i] == null)
            {
                continue;
            }
            if (IsLinked(neighbours[i]))
            {
                directions.Add(IndexToSide(i));
            }
        }

        return directions;

        CellSide IndexToSide(int index)
        {
            if (index == 0)
            {
                return CellSide.North;
            }
            else if (index == 1)
            {
                return CellSide.East;
            }
            else if (index == 2)
            {
                return CellSide.South;
            }
            else if (index == 3)
            {
                return CellSide.West;
            }
            else
            {
                throw new System.Exception("Wrond cell side index");
            }
        }
    }
}
