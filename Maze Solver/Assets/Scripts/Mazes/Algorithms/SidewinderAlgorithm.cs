using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidewinderAlgorithm : IMazeCarver
{
    private Grid _grid;

    public SidewinderAlgorithm(Grid grid)
    {
        _grid = grid;
    }

    public void CarveMaze()
    {
        foreach (var row in _grid.EachRow())
        {
            List<Cell> run = new List<Cell>();

            foreach (var cell in row)
            {
                run.Add(cell);

                bool atEastBoundary = cell.East == null;
                bool atNorthBoundary = cell.North == null;
                bool shouldCloseRun = atEastBoundary || (!atNorthBoundary && Random.Range(0, 2) == 0);

                if (shouldCloseRun)
                {
                    Cell member = Sample(run);
                    if (member.North != null)
                    {
                        member.Link(member.North);
                    }
                    run.Clear();
                }
                else
                {
                    cell.Link(cell.East);
                }
            }
        }
    }

    private Cell Sample(List<Cell> cells)
    {
        return cells[Random.Range(0, cells.Count)];

    }
}
