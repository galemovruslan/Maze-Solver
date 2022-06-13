using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleGridView : GridView
{


    public TriangleGridView(Grid grid, CellView cellPrefab, Transform parentTransform, ICellFactory cellFactory)
        : base(grid, cellPrefab, parentTransform, cellFactory)
    {
    }

    public override Vector3 GetCenterOffCell(int row, int col)
    {
        TriangleCellView triangleCell = _cellPrefab as TriangleCellView;
        float width = triangleCell.Width;
        float height = triangleCell.Height;
        return new Vector3(row * height + height / 2f, 0, col * width / 2 + width / 2);
    }

    public override void SpawnCells()
    {
        TriangleCellView triangleCell = _cellPrefab as TriangleCellView;
        float width = triangleCell.Width;
        float height = triangleCell.Height;
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                float cX = row * height;
                float cZ = col * width / 2f;

                Vector3 spawnPosition = new Vector3(
                    _parentTransform.position.x + cX,
                    _parentTransform.position.y,
                    _parentTransform.position.z + cZ);

                CellView newCellView = _cellFactory.Spawn(spawnPosition);
                newCellView.transform.parent = _parentTransform;
                newCellView.Init(_grid.GetCellAt(row, col));
                _cellViews[row, col] = newCellView;
            }
        }
    }
}
