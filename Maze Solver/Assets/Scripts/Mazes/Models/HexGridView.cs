using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridView : GridView
{


    public HexGridView(Grid grid, CellView cellPrefab, Transform parentTransform, ICellFactory cellFactory)
        : base(grid, cellPrefab, parentTransform, cellFactory)
    {
    }

    public override Vector3 GetCenterOffCell(int row, int col)
    {
        HexCellView hexPrefab = _cellPrefab as HexCellView;
        float size = hexPrefab.Width;
        float aSize = hexPrefab.ASize;
        float bSize = hexPrefab.BSize;
        float height = hexPrefab.NorthToSouth;
        float width = hexPrefab.EastToWest;

        float rowOffset = row * height + bSize;
        float colOffset = col * (width-aSize) + 2 * aSize;

        if(col % 2 != 0)
        {
            rowOffset += bSize;
        }
        return new Vector3(
            rowOffset, 
            0,
            colOffset);
    }

    public override void SpawnCells()
    {
        HexCellView hexPrefab = _cellPrefab as HexCellView;
        float size = hexPrefab.Width;
        float aSize = hexPrefab.ASize;
        float bSize = hexPrefab.BSize;
        float height = hexPrefab.NorthToSouth;
        float width = hexPrefab.EastToWest;

        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                float cZ = size + 3 * col * aSize;
                float cX = bSize + row * height;
                if (col % 2 != 0)
                {
                    cX += bSize;
                }

                Vector3 spawnPosition = new Vector3(
                    _parentTransform.position.x + cX - height / 2f,
                    _parentTransform.position.y,
                    _parentTransform.position.z + cZ - width / 2f);

                CellView newCellView = _cellFactory.Spawn(spawnPosition);
                newCellView.transform.parent = _parentTransform;
                newCellView.Init(_grid.GetCellAt(row, col));
                _cellViews[row, col] = newCellView;
            }
        }
    }
}
