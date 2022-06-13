using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexConfusionHintDisplayer : ConfusionHintDisplayer
{
    public HexConfusionHintDisplayer(GridView gridView) : base(gridView)
    {
    }

    public override void ShowConfusion()
    {
        base.ShowConfusion();
    }

    protected override Vector3 GetRandomDirection(int startRow, int startCol)
    {

        List<Vector2Int> neighbourDirections = new List<Vector2Int>(8);
        for (int row = -1; row <= 1; row++)
        {
            for (int col = -1; col <= 1; col++)
            {
                if (row == 0 && col == 0)
                {
                    continue;
                }

                if ((startRow + startCol) % 2 == 0) // קועםûו סעמכבצû
                {
                    if (row == 1 && (col == -1 || col == 1))
                    {
                        continue;
                    }
                }
                else // םוקועםûו סעמכבצû
                {
                    if (row == -1 && (col == -1 || col == 1))
                    {
                        continue;
                    }
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
                neighbourDirections.Add(new Vector2Int(row, col));

            }
        }

        int randomIndex = Random.Range(0, neighbourDirections.Count);
        var selectedNeighbour = _gridView.CellAt(startRow + neighbourDirections[randomIndex].x, startCol + neighbourDirections[randomIndex].y);
        var currentCell = _gridView.CellAt(startRow, startCol);

        return (selectedNeighbour.transform.position - currentCell.transform.position).normalized;
    }


}
