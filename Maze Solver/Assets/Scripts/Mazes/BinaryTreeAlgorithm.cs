using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BinaryTreeAlgorithm : IMazeCarver
{

    private Grid _grid;

    public BinaryTreeAlgorithm(Grid grid)
    {
        _grid = grid;
    }

    public void CarveMaze()
    {
        foreach (var cell in _grid.EachCell())
        {
            List<Cell> neighbours = new List<Cell>();

            if(cell.North != null)
            {
                neighbours.Add(cell.North);
            }
            if (cell.East != null)
            {
                neighbours.Add(cell.East);
            }

            int randIndex = Random.Range(0, neighbours.Count);
            var neighbour = neighbours[randIndex];
            cell.Link(neighbour);
        }
    }
}
