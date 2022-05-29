using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridComposer : MonoBehaviour
{
    [SerializeField] private int _rows;
    [SerializeField] private int _cols;
    [SerializeField] private CellView _cellPrefab;

    private IMazeCarver _mazeCarver;
    private Grid _grid;
    private CellView[,] _cellViews;
    private CellFactory _cellFactory;
    private IExitPlacementStrategy _exitPlacer;

    private void Awake()
    {
        _grid = new Grid(_rows, _cols);
        _mazeCarver = new SidewinderAlgorithm(_grid);
        _exitPlacer = new RandomBorderExitPlacer(_grid);

        _cellFactory = new CellFactory(_cellPrefab, this.transform);
        _cellViews = new CellView[_rows, _cols];
        MakeMaze();
    }

    private void MakeMaze()
    {
        _mazeCarver.CarveMaze();
        _exitPlacer.PlaceExit();
        SpawnCells();
    }

    private void SpawnCells()
    {
        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _cols; col++)
            {
                Vector3 spawnPosition = new Vector3(
                    transform.position.x + row * _cellPrefab.Width,
                    transform.position.y,
                    transform.position.z + col * _cellPrefab.Width);

                CellView newCellView = _cellFactory.Spawn(spawnPosition);
                newCellView.transform.parent = transform;
                newCellView.Init(_grid.GetCellAt(row, col));
                _cellViews[row, col] = newCellView;
            }
        }
    }
}
