using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DijkstraAlgorythm
{
    private Distances _distances;
    private Dictionary<Cell, Cell> _parents;

    public List<Cell> FindShortestPath(Cell start, Cell goal)
    { 
        FillDistances(start);
        var path = PathToGoal(start, goal);
        return path;
    }

    public List<Cell> FindLongestPath(Cell start)
    {
        Cell pathStart = FarthestFrom(start);
        Cell pathEnd = FarthestFrom(pathStart);
        List<Cell> path = FindShortestPath(pathStart, pathEnd);
        return path;
    }

    private void FillDistances(Cell start)
    {
        _parents = new Dictionary<Cell, Cell>();
        _distances = new Distances(start);
        var frontier = new Queue<Cell>();
        frontier.Enqueue(start);
        while (frontier.Count > 0)
        {
            Cell current = frontier.Dequeue();
            foreach (var linked in current.Links())
            {
                int currentDistanceToLinked = _distances[current] + 1;
                if (_distances.Contains(linked))
                {
                    if (currentDistanceToLinked > _distances[linked])
                    {
                        continue;
                    }
                }
                _parents[linked] = current;
                _distances[linked] = currentDistanceToLinked;
                frontier.Enqueue(linked);
            }
        }
    }

    private List<Cell> PathToGoal(Cell start, Cell goal)
    {
        var current = goal;
        List<Cell> path = new List<Cell>();

        while (current != start)
        {
            path.Add(current);
            current = _parents[current];
        }

        path.Reverse();
        return path;
    }

    private Cell FarthestFrom(Cell start)
    {
        FillDistances(start);
        return FindMaxDistanceCell(start);
    }

    private Cell FindMaxDistanceCell(Cell start)
    {
        Cell maxCell = start;
        int maxDistance = 0;

        foreach (Cell cell in _distances.Cells())
        {
            if (_distances[cell] > maxDistance)
            {
                maxCell = cell;
                maxDistance = _distances[cell];
            }
        }
        return maxCell;
    }
}
