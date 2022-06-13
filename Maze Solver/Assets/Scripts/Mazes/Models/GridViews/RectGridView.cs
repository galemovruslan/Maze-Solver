using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectGridView : GridView
{

    public RectGridView(Grid grid, CellView cellPrefab, Transform parentTransform, CellFactory cellFactory)
        : base(grid, cellPrefab, parentTransform, cellFactory)
    {
    }

    public override Vector3 GetCenterOffCell(int row, int col)
    {
        return new Vector3(row * _cellPrefab.Width + _cellPrefab.Width / 2f,
            0,
            col * _cellPrefab.Width + _cellPrefab.Width / 2f);
    }

    public override void SpawnCells()
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                Vector3 spawnPosition = new Vector3(
                    _parentTransform.position.x + row * _cellPrefab.Width,
                    _parentTransform.position.y,
                    _parentTransform.position.z + col * _cellPrefab.Width);

                CellView newCellView = _cellFactory.Spawn(spawnPosition);
                newCellView.transform.parent = _parentTransform;
                newCellView.Init(_grid.GetCellAt(row, col));
                _cellViews[row, col] = newCellView;
            }
        }
    }
}
