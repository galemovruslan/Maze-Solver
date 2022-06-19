using System;
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

    [SerializeField] protected CellView _cellPrefab;
    [SerializeField] protected GoalTrigger _triggerPrefab;

    protected int _rows;
    protected int _cols;
    protected Grid _grid;
    protected CellFactory _cellFactory;
    protected GridView _gridView;
    protected PathDisplayer _hintDisplayer;

    private IMazeCarver _mazeCarver;
    private DijkstraAlgorythm _mazeSolver;
    private IPathCreatePolicy _pathCreatePolicy;
    private Vector2Int _startCoords;
    private PathMaker _pathMaker;
    private GoalTrigger _trigger;
    //protected virtual void Awake()
    //{
    //    _mazeCarver = new RecursiveBackTrackerAlgorithm(_grid);
    //    _mazeSolver = new DijkstraAlgorythm();
    //    _pathCreatePolicy = new MaxLengthPathPolicy(_grid, _mazeSolver);
    //    _pathMaker = new PathMaker(_grid, _mazeSolver, _pathCreatePolicy);

    //    MakeMaze();
    //}

    public virtual void Initialize(int rows, int cols, Func<GridView, PathDisplayer> hintSolver, Func<Grid, IMazeCarver> mazeCarverSolver)
    {
        _rows = rows;
        _cols = cols;
        _mazeCarver = mazeCarverSolver(_grid);
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

        SetGoalTrigger(goal);
        _gridView.SpawnCells();
        _hintDisplayer.MarkPathEnds(start, goal);
        _hintDisplayer.DisplayHint(path);
    }

    private void SetGoalTrigger(Cell goal)
    {
        var goalWorldPosition = new Vector3(0, transform.position.y, 0) + _gridView.GetCenterOffCell(goal.Row, goal.Column);
        _trigger = Instantiate<GoalTrigger>(_triggerPrefab, goalWorldPosition, Quaternion.identity);
        _trigger.SetRadius(_cellPrefab.Width / 1.66f / 2f);
    }
    
    public void SetWinAction(Action onWin)
    {
        _trigger.OnGoalAchived += onWin;
    }

}
