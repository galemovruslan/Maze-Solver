using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RectGridComposer : GridComposer
{
    public override void Initialize(int rows, int cols, Func<GridView, PathDisplayer> hintSolver, Func<Grid,IMazeCarver> mazeCarverSolver)
    {
        _grid = new Grid(rows, cols);
        _cellFactory = new CellFactory(_cellPrefab);
        _gridView = new RectGridView(_grid, _cellPrefab, this.transform, _cellFactory);
        _hintDisplayer = hintSolver(_gridView);
        base.Initialize(rows, cols, hintSolver, mazeCarverSolver);
    }
}
