using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMaker 
{
    private Grid _grid;
    private DijkstraAlgorythm _mazeSolver;
    private IPathCreatePolicy _pathCreatePolicy;

    public PathMaker(Grid grid, 
        DijkstraAlgorythm mazeSolver, 
        IPathCreatePolicy pathCreatePolicy)
    {
        _grid = grid;
        _mazeSolver = mazeSolver;
        _pathCreatePolicy = pathCreatePolicy;
    }

    public List<Cell> MakePath()
    {
        _pathCreatePolicy.GetPathEndPoints();
        Cell start = _pathCreatePolicy.Start;
        Cell goal = _pathCreatePolicy.End;
        List<Cell> path = _mazeSolver.FindShortestPath(start, goal);
        return path;
    }
}
