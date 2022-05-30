using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConcreteBorderExitPlacer : BorderExitPlacer
{
    private Vector2Int _exitCoord;

    public ConcreteBorderExitPlacer(Grid grid, Vector2Int exitCoord) : base(grid)
    {
        _exitCoord = exitCoord;
    }

    public override void PlaceExit()
    {
        var borderCoordnates = GetListOfBorderCells();
        if (!borderCoordnates.Contains(_exitCoord))
        {
            throw new System.Exception("Exit coordiantes are not at maze border");
        }

        MarkCellAsExit(_exitCoord);
    }
}
