using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCell : Cell
{
    public bool Upright => _upright; 

    private bool _upright;

    public TriangleCell(int row, int col) : base(row, col)
    {
        _upright = (row + col) % 2 == 0;
    }


    public override List<Cell> Neighbours()
    {
        List<Cell> neighbours = new List<Cell>();
        neighbours.Add(East);
        neighbours.Add(West);
        if (_upright)
        {
            neighbours.Add(South);
        }
        else
        {
            neighbours.Add(North);
        }
        return neighbours;
    }

}
