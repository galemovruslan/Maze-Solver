using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AldousBroderAlgorithm : IMazeCarver
{

    private Grid _grid;

    public AldousBroderAlgorithm(Grid grid)
    {
        _grid = grid;
    }

    public void CarveMaze()
    {
        Cell current = _grid.RandomCell();
        int unvisited = _grid.Size() - 1;

        while (unvisited > 0)
        {
            Cell neighbour = GetRamdomNeighbour(current);

            if(neighbour.Links().Count == 0)
            {
                neighbour.Link(current);
                unvisited--;
            }
            current = neighbour;
        }
    }

    private Cell GetRamdomNeighbour(Cell current)
    {
        Cell randomNeighbour = null;

        while(randomNeighbour == null)
        {
            var neighbours = current.Neighbours();
            randomNeighbour = neighbours[Random.Range(0, neighbours.Count)];
        }

        return randomNeighbour;
    }
}
