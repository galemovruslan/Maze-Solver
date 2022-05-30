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

        Vector2Int exitCoord = borderCoords[cellIdx];
        MarkCellAsExit(exitCoord);
    }
}
