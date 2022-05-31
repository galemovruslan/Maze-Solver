using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WilsonsAlgorithm : IMazeCarver
{
    private Grid _grid;

    public WilsonsAlgorithm(Grid grid)
    {
        _grid = grid;
    }

    public void CarveMaze()
    {
        List<Cell> unvisited = _grid.EachCell().ToList();

        Cell first = SampleCell(unvisited);
        unvisited.Remove(first);

        while (unvisited.Count > 0)
        {
            List<Cell> path = new List<Cell>();
            Cell cell = SampleCell(unvisited);
            path.Add(cell);

            while (unvisited.Contains(cell))
            {
                cell = SampleCell(cell.Neighbours());
                if (path.Contains(cell))
                {
                    int position = path.IndexOf(cell);
                    path = path.Take(position + 1).ToList();
                }
                else
                {
                    path.Add(cell);
                }
            }
            for (int i = 0; i < path.Count - 1; i++)
            {
                path[i].Link(path[i + 1]);
                unvisited.Remove(path[i]);
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
