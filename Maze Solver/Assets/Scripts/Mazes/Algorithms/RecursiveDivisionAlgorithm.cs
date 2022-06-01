using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RecursiveDivisionAlgorithm : IMazeCarver
{
    private Grid _grid;
    int _depth;
    public RecursiveDivisionAlgorithm(Grid grid)
    {
        _grid = grid;
    }

    public void CarveMaze()
    {
        foreach (var cell in _grid.EachCell())
        {
            List<Cell> available_neighbours = cell.Neighbours().Where(n => n != null).ToList();
            available_neighbours.ForEach(neighbour => cell.Link(neighbour, false));
        }

       Divide(0, 0, _grid.Rows, _grid.Cols);

    }

    private void Divide(int row, int col, int height, int width)
    {
        if (height <= 1 || width <= 1)
        {
            return;
        }

        if (height > width)
        {
            DivideHorizontaly(row, col, height, width);
        }
        else
        {
            DivideVerticaly(row, col, height, width);
        }
    }

    private void DivideVerticaly(int row, int col, int height, int width)
    {
        int divideEastOf = UnityEngine.Random.Range(0, width - 1);
        int passageAt = UnityEngine.Random.Range(0, height);

        for (int y = 0; y < height; y++)
        {
            if (passageAt == y) { continue; }

            Cell cell = _grid.GetCellAt(row + y, col + divideEastOf);
            if (cell == null)
            {
                continue;
            }
            cell.Unlink(cell.East);
        }

        Divide(row, col, height, divideEastOf + 1);
        Divide(row, col + divideEastOf + 1, height, width - divideEastOf - 1);
    }

    private void DivideHorizontaly(int row, int col, int height, int width)
    {
        int divideSouthOf = UnityEngine.Random.Range(0, height - 1);
        int passageAt = UnityEngine.Random.Range(0, width);

        for (int x = 0; x < width; x++)
        {
            if (passageAt == x) { continue; }

            Cell cell = _grid.GetCellAt(row + divideSouthOf, col + x);
            if (cell == null)
            {
                continue;
            }
            cell.Unlink(cell.South);
        }

        Divide(row, col, divideSouthOf + 1, width);
        Divide(row + divideSouthOf + 1, col, height - divideSouthOf - 1, width);
    }

}
