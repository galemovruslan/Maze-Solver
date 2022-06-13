using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Cell
{
    protected struct Neighbour
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
    public CellSide ExitSide { get; set; }


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

    public virtual List<Cell> Neighbours()
    {
        return new List<Cell>() { _north, _east, _south, _west };
    }

    public List<CellSide> LinkDirections()
    {
        var directions = new List<CellSide>();
        var neighbours = SidedNeighbours();

        for (int i = 0; i < neighbours.Count; i++)
        {
            if (neighbours[i].cell == null)
            {
                continue;
            }
            if (IsLinked(neighbours[i].cell))
            {
                directions.Add(neighbours[i].side);
            }
        }
        if(ExitSide != CellSide.None)
        {
            directions.Add(ExitSide);
        }
        return directions;
    }

    protected virtual List<Neighbour> SidedNeighbours()
    {
        return new List<Neighbour>() 
        {
            new Neighbour() {cell = _north, side = CellSide.North},
            new Neighbour() {cell = _east, side = CellSide.East},
            new Neighbour() {cell = _south, side = CellSide.South},
            new Neighbour() {cell = _west, side = CellSide.West}
        };
    }
}
