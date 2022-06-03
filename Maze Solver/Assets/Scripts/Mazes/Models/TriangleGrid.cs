using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleGrid : Grid
{
    public TriangleGrid(int rows, int cols) : base(rows, cols)
    {
    }

    protected override void ConfigureCels()
    {
        foreach (var cell in EachCell())
        {
            TriangleCell triangleCell = cell as TriangleCell;

            int row = triangleCell.Row;
            int col = triangleCell.Column;

            triangleCell.West = GetCellAt(row, col - 1);
            triangleCell.East = GetCellAt(row, col + 1);

            if (triangleCell.Upright)
            {
                triangleCell.South = GetCellAt(row + 1, col);
            }
            else
            {
                triangleCell.North = GetCellAt(row - 1, col);
            }


        }
    }

    protected override void PrepareGrid()
    {
        _grid = new TriangleCell[_rows, _cols];
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                _grid[row, col] = new TriangleCell(row, col);
            }
        }
    }
}
