using UnityEngine;

public class RandomBorderExitPlacer : BorderExitPlacer
{
    public RandomBorderExitPlacer(Grid grid) : base(grid)
    {
    }

    public override void PlaceExit()
    {
        var borderCoords = GetListOfBorderCells();
        int cellIdx = Random.Range(0, borderCoords.Count);

        var exitCoord = borderCoords[cellIdx];
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

        _grid.GetCellAt(exitCoord.x, exitCoord.y).ExitSide = exitSide;
        Debug.Log($"Exit at {exitCoord} directed at {exitSide}");
    }
}
