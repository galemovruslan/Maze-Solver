using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectGridComposer : GridComposer
{
    protected override void Awake()
    {
        _grid = new Grid(_rows, _cols);
        _cellFactory = new CellFactory(_cellPrefab);
        _gridView = new RectGridView(_grid, _cellPrefab, this.transform, _cellFactory);

        base.Awake();
    }
}
