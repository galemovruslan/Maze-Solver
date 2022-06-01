using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PrimsAlgorithm : IMazeCarver
{

    private Grid _grid;

    public PrimsAlgorithm(Grid grid)
    {
        _grid = grid;
    }

    public void CarveMaze()
    {
        HashSet<Cell> active = new HashSet<Cell>();
        Cell start = _grid.RandomCell();
        active.Add(start);

        Dictionary<Cell, int> costs = new Dictionary<Cell, int>();
        foreach (var cell in _grid.EachCell())
        {
            costs.Add(cell, Random.Range(0, 100));
        }

        while(active.Count> 0)
        {
            Cell current = active.OrderBy(cell => costs[cell]).First();
            List<Cell> available_neighbours = current.Neighbours().Where(n => n != null && n.Links().Count == 0).ToList(); 

            if(available_neighbours.Count > 0)
            {
                Cell neighbour = available_neighbours.OrderBy(cell => costs[cell]).First();
                current.Link(neighbour);
                active.Add(neighbour);
            }
            else
            {
                active.Remove(current);
            }
        }
    }

 

}
