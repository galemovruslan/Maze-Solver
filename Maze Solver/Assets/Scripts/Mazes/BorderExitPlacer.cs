using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BorderExitPlacer : IExitPlacementStrategy
{
    protected Grid _grid;

    public BorderExitPlacer(Grid grid)
    {
        _grid = grid;
    }

    public abstract void PlaceExit();
    

    protected List<Vector2Int> GetListOfBorderCells()
    {
        List<Vector2Int> borderCoords = new List<Vector2Int>();

        for (int row = 0; row < _grid.Rows; row++)
        {
            for (int col = 0; col < _grid.Cols; col++)
            {
                if (row == 0 || col == 0 || row == _grid.Rows - 1 || col == _grid.Cols - 1)
                {
                    borderCoords.Add(new Vector2Int(row, col));
                }
            }
        }
        return borderCoords;
    }
}
