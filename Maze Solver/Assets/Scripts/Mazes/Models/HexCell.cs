using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell : Cell
{
    public Cell NorthEast { get=>_northEast; set =>_northEast = value;} 
    public Cell SouthWest { get => _southWest; set => _southWest = value; } 
    public Cell SouthEast { get => _southEast; set => _southEast = value; }
    public Cell NorthWest { get => _northWest; set => _northWest = value; }


    private Cell _northWest;
    private Cell _northEast;
    private Cell _southWest;
    private Cell _southEast;

    public HexCell(int row, int col) : base(row, col)
    {
    }

    public override List<Cell> Neighbours()
    {
        return new List<Cell>() { North, _northEast, _southEast, South, _southWest, _northWest };
    }

    protected override List<Neighbour> SidedNeighbours()
    {
        return new List<Neighbour>()
        {
            new Neighbour() {cell = North, side = CellSide.North},
            new Neighbour() {cell = _northEast, side = CellSide.NorthEast},
            new Neighbour() {cell = _southEast, side = CellSide.SouthEast},
            new Neighbour() {cell = South, side = CellSide.South},
            new Neighbour() {cell = _southWest, side = CellSide.SouthWest},
            new Neighbour() {cell = _northWest, side = CellSide.NorthWest},
        };
    }
}
