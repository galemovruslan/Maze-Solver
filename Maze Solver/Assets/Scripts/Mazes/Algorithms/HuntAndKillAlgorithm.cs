using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HuntAndKillAlgorithm : IMazeCarver
{
    Grid _grid;

    public HuntAndKillAlgorithm(Grid grid)
    {
        _grid = grid;
    }
    public void CarveMaze()
    {
        Cell current = _grid.RandomCell();

        while (current != null)
        {
            List<Cell> unvisitedNeighbours = current.Neighbours().Where(n => n != null && n.Links().Count == 0).ToList();

            if (unvisitedNeighbours.Count > 0)
            {
                Cell neighbour = SampleCell(unvisitedNeighbours);
                current.Link(neighbour);
                current = neighbour;
            }
            else
            {
                current = null;

                foreach (var cell in _grid.EachCell())
                {
                    List<Cell> visitedNeighbours = cell.Neighbours().Where(n => n != null && n.Links().Count != 0).ToList();
                    if (cell.Links().Count == 0 && visitedNeighbours.Count > 0)
                    {
                        current = cell;

                        Cell neighbour = SampleCell(visitedNeighbours);
                        current.Link(neighbour);
                        break;
                    }
                }
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
