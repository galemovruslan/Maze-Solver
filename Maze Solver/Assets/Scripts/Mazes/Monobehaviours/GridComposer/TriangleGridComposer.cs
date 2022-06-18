using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleGridComposer : GridComposer
{
    public override void Initialize(Func<GridView, PathDisplayer> hintSolver, Func<Grid, IMazeCarver> mazeCarverSolver)
    {
        _grid = new TriangleGrid(_rows, _cols);
        _cellFactory = new CellFactory(_cellPrefab);
        _gridView = new TriangleGridView(_grid, _cellPrefab, this.transform, _cellFactory);
        _hintDisplayer = hintSolver(_gridView);
        base.Initialize(hintSolver, mazeCarverSolver);
    }
}
