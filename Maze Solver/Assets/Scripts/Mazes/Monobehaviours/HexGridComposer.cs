using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGridComposer : GridComposer
{
    protected override void Awake()
    {
        _grid = new HexGrid(_rows, _cols);
        _cellFactory = new CellFactory(_cellPrefab);
        _gridView = new HexGridView(_grid, _cellPrefab, this.transform, _cellFactory);

        base.Awake();
    }
}
