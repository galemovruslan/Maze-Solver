using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxLengthPathPolicy : IPathCreatePolicy
{
    public Cell Start { get; private set; }
    public Cell End { get; private set; }

    private Grid _grid;
    private DijkstraAlgorythm _pathSolver;

    public MaxLengthPathPolicy(Grid grid, DijkstraAlgorythm pathSolver)
    {
        _grid = grid;
        _pathSolver = pathSolver;
    }

    public void GetPathEndPoints()
    {
        List<Cell> path = _pathSolver.FindLongestPath(_grid.RandomCell());

        Start = path[0];
        End = path[path.Count - 1];
    }
}

