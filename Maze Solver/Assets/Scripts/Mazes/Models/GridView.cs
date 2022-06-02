using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridView
{
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


    public void ShowPath(List<Cell> path)
    {
        for (int i = 0; i < path.Count-1; i++)
        {
            var item = path[i];
            int toNextRow = path[i + 1].Row  - item.Row;
            int toNextCol = path[i + 1].Column - item.Column;
            Vector3 toNext = new Vector3(toNextRow, 0, toNextCol).normalized;
            _cellViews[item.Row, item.Column].ShowHint(true, toNext);
        }
    }

    public void MarkPathEnds(Cell start, Cell goal)
    {
        _cellViews[start.Row, start.Column].SetStart();
        _cellViews[goal.Row, goal.Column].SetGoal();
    }

    public abstract void SpawnCells();

    public abstract Vector3 GetCenterOffCell(int row, int col);
    
}
