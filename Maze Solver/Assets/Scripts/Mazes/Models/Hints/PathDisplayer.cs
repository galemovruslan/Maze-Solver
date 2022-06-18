using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDisplayer
{
    protected GridView _gridView;

    public PathDisplayer(GridView gridView)
    {
        _gridView = gridView;
    }
    public void MarkPathEnds(Cell start, Cell goal)
    {
        _gridView.CellAt(start.Row, start.Column).SetStart();
        _gridView.CellAt(goal.Row, goal.Column).SetGoal();
    }

    public virtual void DisplayHint(List<Cell> path)
    {
        ShowPath(path);
    }

    public void ShowPath(List<Cell> path)
    {
        for (int i = 0; i < path.Count - 1; i++)
        {
            var cellOnPath = path[i];
            int nextRow = path[i + 1].Row;
            int nextCol = path[i + 1].Column;
            CellView nextCell = _gridView.CellAt(nextRow, nextCol);
            CellView currentCell = _gridView.CellAt(cellOnPath.Row, cellOnPath.Column);
            Vector3 toNext = nextCell.transform.position - currentCell.transform.position;
            _gridView.CellAt(cellOnPath.Row, cellOnPath.Column).ShowHint(true, toNext);
        }
    }

}
