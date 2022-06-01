using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridComposer : MonoBehaviour
{

    public Vector3 StartPosition
    {
        get
        {
            return new Vector3(_startCoords.x * _cellPrefab.Width + _cellPrefab.Width / 2f,
                transform.position.y,
                _startCoords.y * _cellPrefab.Width + _cellPrefab.Width / 2f);
        }
    }

    [SerializeField] private int _rows;
    [SerializeField] private int _cols;
    [SerializeField] private CellView _cellPrefab;

    private IMazeCarver _mazeCarver;
    private Grid _grid;
    private CellView[,] _cellViews;
    private CellFactory _cellFactory;
    private IExitPlacementStrategy _exitPlacer;
    private DijkstraAlgorythm _mazeSolver;
    private IPathCreatePolicy _pathCreatePolicy;
    private Vector2Int _startCoords;
    private void Awake()
    {
        _grid = new Grid(_rows, _cols);
        _mazeCarver = new EllersAlgorithm(_grid);
        _mazeSolver = new DijkstraAlgorythm();
        _pathCreatePolicy = new MaxLengthPathPolicy(_grid, _mazeSolver);
        _cellFactory = new CellFactory(_cellPrefab, this.transform);
        _cellViews = new CellView[_rows, _cols];

        MakeMaze();
    }

    private void ShowPath(List<Cell> path)
    {
        foreach (var item in path)
        {
            _cellViews[item.Row, item.Column].ShowPoint(true);
        }
    }

    private void MakeMaze()
    {
        var path = PrepareModels();
        PrepareVisuals(path);
    }

    private void PrepareVisuals(List<Cell> path)
    {
        
        SpawnCells();
        ShowPath(path);
        SetPathEnds();
    }

    private List<Cell> PrepareModels()
    {
        _mazeCarver.CarveMaze();
        _pathCreatePolicy.GetPathEndPoints();
        Cell start = _pathCreatePolicy.Start;
        Cell goal = _pathCreatePolicy.End;
        _startCoords = new Vector2Int(start.Row, start.Column);
        _exitPlacer = new ArbitraryExitPlacer();
        _exitPlacer.PlaceExit();
        List<Cell> path = _mazeSolver.FindShortestPath(start, goal);
        return path;
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

    private void SetPathEnds()
    {
        Cell start = _pathCreatePolicy.Start;
        _cellViews[start.Row, start.Column].SetStart();

        Cell goal = _pathCreatePolicy.End;
        _cellViews[goal.Row, goal.Column].SetGoal();
    }
}
