using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfusionPathDisplayer : PathDisplayer
{
    public ConfusionPathDisplayer(GridView gridView) : base(gridView)
    {
    }

    public override void DisplayHint(List<Cell> path)
    {
        ShowConfusion();
        base.DisplayHint(path);
    }


    public virtual void ShowConfusion()
    {
        for (int row = 0; row < _gridView.Rows; row++)
        {
            for (int col = 0; col < _gridView.Cols; col++)
            {

                var cell = _gridView.CellAt(row, col);
                var direction = GetRandomDirection(row, col);
                _gridView.CellAt(row, col).ShowHint(true, direction.normalized);
            }
        }
    }

    protected virtual Vector3 GetRandomDirection(int startRow, int startCol)
    {
        List<Vector2Int> directions = new List<Vector2Int>(8);
        for (int row = -1; row <= 1; row++)
        {
            for (int col = -1; col <= 1; col++)
            {
                if (Mathf.Abs(row) == Mathf.Abs(col))
                {
                    continue;
                }

                int neighbourRow = startRow + row;
                int neighbourCol = startCol + col;

                if (
                    neighbourRow < 0 ||
                    neighbourCol < 0 ||
                    neighbourRow >= _gridView.Rows ||
                    neighbourCol >= _gridView.Cols
                    )
                {
                    continue;
                }
                directions.Add(new Vector2Int(row, col));
            }
        }

        int randomIndex = Random.Range(0, directions.Count);
        return new Vector3(directions[randomIndex].x, 0, directions[randomIndex].y);
    }


}
