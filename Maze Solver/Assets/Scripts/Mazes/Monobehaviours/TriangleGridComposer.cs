using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleGridComposer : GridComposer
{
    protected override void Awake()
    {
        _grid = new TriangleGrid(_rows, _cols);
        _cellFactory = new CellFactory(_cellPrefab);
        _gridView = new TriangleGridView(_grid, _cellPrefab, this.transform, _cellFactory);

        base.Awake();
    }
}
