using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecursiveBackTrackerAlgorithm : IMazeCarver
{

    Grid _grid;

    public RecursiveBackTrackerAlgorithm(Grid grid)
    {
        _grid = grid;
    }

    public void CarveMaze()
    {
        Stack<Cell> pathHistory = new Stack<Cell>();

        pathHistory.Push(_grid.RandomCell());

        while(pathHistory.Count > 0)
        {
            Cell current = pathHistory.Peek();

            List<Cell> unvisitedNeighbours = current.Neighbours().Where(n => n != null && n.Links().Count == 0).ToList();
            if (unvisitedNeighbours.Count == 0)
            {
                pathHistory.Pop();
            }
            else
            {
                Cell neighbour = SampleCell(unvisitedNeighbours);
                current.Link(neighbour);
                pathHistory.Push(neighbour);
            }
        }
    }

    private Cell SampleCell(List<Cell> list)
    {
        int idx;
        Cell cell;
        do
        {
            idx = Random.Range(0, list.Count);
            cell = list[idx];
        } while (cell == null);
        return cell;
    }
}
