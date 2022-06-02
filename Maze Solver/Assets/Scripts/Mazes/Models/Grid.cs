using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public int Rows => _rows;
    public int Cols => _cols; 

    protected int _rows;
    protected int _cols;
    protected Cell[,] _grid;

    public Grid(int rows, int cols)
    {
        _rows = rows;
        _cols = cols;

        PrepareGrid();
        ConfigureCels();
    }

    public Cell RandomCell()
    {
        int rowIndex = Random.Range(0, _rows);
        int colIndex = Random.Range(0, _cols);
        return _grid[rowIndex, colIndex];
    }

    public int Size()
    {
        return _rows * _cols;
    }

    public IEnumerable<List<Cell>> EachRow()
    {
        for (int row = 0; row < _rows; row++)
        {
            List<Cell> gridRow = new List<Cell>(_cols);

            for (int col = 0; col < _cols; col++)
            {
                gridRow.Add(_grid[row, col]);
            }
            yield return gridRow;
        }
    }

    public IEnumerable<Cell> EachCell()
    {
        foreach (var gridRow in EachRow())
        {
            foreach (var cell in gridRow)
            {
                yield return cell;
            }
        }
    }
    public Cell GetCellAt(int row, int col)
    {
        if (row >= _rows || col >= _cols || row < 0 || col < 0)
        {
            return null;
        }
        return _grid[row, col];
    }
    protected virtual void PrepareGrid()
    {
        _grid = new Cell[_rows, _cols];
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                _grid[row, col] = new Cell(row, col);
            }
        }
    }

    protected virtual void ConfigureCels()
    {
        foreach (var cell in EachCell())
        {
            int row = cell.Row;
            int col = cell.Column;

            cell.North = GetCellAt(row - 1, col);
            cell.South = GetCellAt(row + 1, col);
            cell.West = GetCellAt(row, col - 1);
            cell.East = GetCellAt(row, col + 1);

        }
    }

}
