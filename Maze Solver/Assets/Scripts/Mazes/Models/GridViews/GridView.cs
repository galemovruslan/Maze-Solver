using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridView
{
    public int Rows => _rows;
    public int Cols => _cols; 

    protected int _rows;
    protected int _cols;
    protected Grid _grid;
    protected CellView _cellPrefab;
    protected Transform _parentTransform;
    protected ICellFactory _cellFactory;
    protected CellView[,] _cellViews;


    public GridView( Grid grid, CellView cellPrefab, Transform parentTransform, ICellFactory cellFactory)
    {
        _grid = grid;
        _rows = _grid.Rows;
        _cols = _grid.Cols;
        _cellPrefab = cellPrefab;
        _parentTransform = parentTransform;
        _cellFactory = cellFactory;
        _cellViews = new CellView[_rows, _cols]; ;

    }

    public CellView CellAt(int row, int col)
    {
        return _cellViews[row, col];
    }

    public abstract void SpawnCells();

    public abstract Vector3 GetCenterOffCell(int row, int col);
    
}
