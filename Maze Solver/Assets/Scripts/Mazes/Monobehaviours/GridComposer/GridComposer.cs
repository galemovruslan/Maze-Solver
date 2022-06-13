using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridComposer : MonoBehaviour
{

    public Vector3 StartPosition
    {
        get
        {
            return new Vector3(0, transform.position.y, 0) + _gridView.GetCenterOffCell(_startCoords.x, _startCoords.y);
        }
    }

    [SerializeField] protected int _rows;
    [SerializeField] protected int _cols;
    [SerializeField] protected CellView _cellPrefab;

    protected Grid _grid;
    protected CellFactory _cellFactory;
    protected GridView _gridView;
    protected HintDisplayer _hintDisplayer;

    private IMazeCarver _mazeCarver;
    private DijkstraAlgorythm _mazeSolver;
    private IPathCreatePolicy _pathCreatePolicy;
    private Vector2Int _startCoords;

    private PathMaker _pathMaker;
    protected virtual void Awake()
    {
        _mazeCarver = new RecursiveBackTrackerAlgorithm(_grid);
        _mazeSolver = new DijkstraAlgorythm();
        _pathCreatePolicy = new MaxLengthPathPolicy(_grid, _mazeSolver);
        _pathMaker = new PathMaker(_grid, _mazeSolver, _pathCreatePolicy);

        MakeMaze();
    }

    private void MakeMaze()
    {
        _mazeCarver.CarveMaze();
        List<Cell> path = _pathMaker.MakePath();
        Cell start = path[0];
        Cell goal = path[path.Count - 1];
        _startCoords = new Vector2Int(start.Row, start.Column);

        _gridView.SpawnCells();
        _hintDisplayer.MarkPathEnds(start, goal);
        _hintDisplayer.DisplayHint(path);
    }

    
}
