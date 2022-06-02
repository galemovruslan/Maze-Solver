using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : Grid
{
    public HexGrid(int rows, int cols) : base(rows, cols)
    {
    }

    protected override void ConfigureCels()
    {
        foreach (var cell in EachCell())
        {
            int row = cell.Row;
            int col = cell.Column;

            int northDiagonal;
            int southDiagonal;
            if(cell.Column % 2 == 0)
            {
                northDiagonal = row - 1;
                southDiagonal = row;
            }
            else
            {
                northDiagonal = row;
                southDiagonal = row + 1;
            }

            HexCell hexCell = cell as HexCell;
            hexCell.North = GetCellAt(row - 1, col);
            hexCell.South = GetCellAt(row + 1, col);
            hexCell.West = GetCellAt(row, col - 1);
            hexCell.East = GetCellAt(row, col + 1);
            hexCell.NorthWest = GetCellAt(northDiagonal, col - 1);
            hexCell.NorthEast = GetCellAt(northDiagonal, col + 1);
            hexCell.SouthWest = GetCellAt(southDiagonal, col - 1);
            hexCell.SouthEast = GetCellAt(southDiagonal, col + 1);
        }
    }

    protected override void PrepareGrid()
    {
        _grid = new HexCell[_rows, _cols];
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                _grid[row, col] = new HexCell(row, col);
            }
        }
    }
}
