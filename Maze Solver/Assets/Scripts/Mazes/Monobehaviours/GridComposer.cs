using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridComposer : MonoBehaviour
{

    public Vector3 StartPosition
    {
        get
        {
            return new Vector3(0, transform.position.y, 0) + _gridView.GetCenterOffCell(_startCoords.x, _startCoords.y);
        }
    }

    [SerializeField] private int _rows;
    [SerializeField] private int _cols;
    [SerializeField] private CellView _cellPrefab;

    private Grid _grid;
    private CellFactory _cellFactory;
    private IMazeCarver _mazeCarver;
    private DijkstraAlgorythm _mazeSolver;
    private IPathCreatePolicy _pathCreatePolicy;
    private Vector2Int _startCoords;

    private PathMaker _pathMaker;
    private GridView _gridView;
    private void Awake()
    {
        _grid = new TriangleGrid(_rows, _cols);
        _mazeCarver = new RecursiveBackTrackerAlgorithm(_grid);
        _mazeSolver = new DijkstraAlgorythm();
        _pathCreatePolicy = new MaxLengthPathPolicy(_grid, _mazeSolver);
        _cellFactory = new CellFactory(_cellPrefab, this.transform);
        _pathMaker = new PathMaker(_grid, _mazeSolver, _pathCreatePolicy);
        _gridView = new TriangleGridView(_grid, _cellPrefab, this.transform, _cellFactory);

        MakeMaze();
    }

    private void MakeMaze()
    {
        _mazeCarver.CarveMaze();
        //List<Cell> path = _pathMaker.MakePath();
        //Cell start = path[0];
        //Cell goal = path[path.Count - 1];
        //_startCoords = new Vector2Int(start.Row, start.Column);

        _gridView.SpawnCells();
        //_gridView.MarkPathEnds(start, goal);
        //_gridView.ShowPath(path);
    }

    
}
