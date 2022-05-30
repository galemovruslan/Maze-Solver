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
    
    protected void MarkCellAsExit(Vector2Int exitCoord)
    {
        CellSide exitSide = DetectSide(exitCoord);
        _grid.GetCellAt(exitCoord.x, exitCoord.y).ExitSide = exitSide;
        //Debug.Log($"Exit at {exitCoord} directed at {exitSide}");
    }

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

    protected CellSide DetectSide(Vector2Int exitCoord)
    {
        CellSide exitSide;
        if (exitCoord.x == 0 && exitCoord.y != 0)
        {
            exitSide = CellSide.North;
        }
        else if (exitCoord.x == _grid.Rows - 1 && exitCoord.y != 0)
        {
            exitSide = CellSide.South;
        }
        else if (exitCoord.x != 0 && exitCoord.y == 0)
        {
            exitSide = CellSide.West;
        }
        else if (exitCoord.x != 0 && exitCoord.y == _grid.Cols - 1)
        {
            exitSide = CellSide.East;
        }
        else
        {
            throw new System.Exception("Wrong grid border coordinates");
        }
        return exitSide;
    }
}
