using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEndsPathPolicy : IPathCreatePolicy
{
    public Cell Start { get; private set; }
    public Cell End { get; private set; }

    private Grid _grid;

    public SetEndsPathPolicy(Grid grid, Cell start, Cell end)
    {
        _grid = grid;
        Start = start;
        End = end;
    }

    public void GetPathEndPoints()
    {

    }
}
