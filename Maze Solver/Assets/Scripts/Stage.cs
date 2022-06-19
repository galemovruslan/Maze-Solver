using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{

    [SerializeField] private List<GridComposer> _composerPrefabs;
    [SerializeField] private CinemachineVirtualCamera _cameraPrefab;
    [SerializeField] private TrailComposer _trailPrefab;
    [SerializeField] private PlayerSpawner _playerSpawnerPrefab;
    [SerializeField] private WinMenu _winMenu;

    private LevelDescription _currentDescription;
    private GridComposer _currentGridComposer;
    private CinemachineVirtualCamera _playerCamera;
    private TrailComposer _playerTrail;
    private PlayerComposer _player;
    private PlayerSpawner _activeSpawner;

    private Func<Grid, IMazeCarver> _carverCreator;
    private Func<GridView, PathDisplayer> _pathDisplayerCreator;

    private void Awake()
    {
        _currentDescription = DataSerializer.Read<LevelDescription>(StringConstants.DescriptionSavePath);
        ResolveCarver(_currentDescription._carverAlgorithm);
        ResolveDifficulty(_currentDescription._difficulty, _currentDescription._cellType);
        SpawmMaze(_currentDescription._cellType);
        SpawnPlayer();
        SpawnCamera(_player.transform);
        SpawnTrail();
    }

    private void SpawnPlayer()
    {
        _activeSpawner = Instantiate<PlayerSpawner>(_playerSpawnerPrefab);
        _activeSpawner.Spawn(_currentGridComposer.StartPosition);
        _player = _activeSpawner.Player.GetComponent<PlayerComposer>();
    }

    private void SpawnTrail()
    {
        TrailBrush brush = _activeSpawner.Player.GetComponent<TrailBrush>();
        _playerTrail = Instantiate<TrailComposer>(_trailPrefab);
        _playerTrail.Initialize(brush);
    }

    private void SpawnCamera(Transform player)
    {
        _playerCamera = Instantiate<CinemachineVirtualCamera>(_cameraPrefab);
        _playerCamera.Follow = player;
        _playerCamera.LookAt = player;
        _playerCamera.DestroyCinemachineComponent<CinemachineComposer>();
    }

    private void SpawmMaze(CellType cellType)
    {
        _currentGridComposer = Instantiate(_composerPrefabs[(int)cellType]);
        _currentGridComposer.Initialize(
            _currentDescription._mazeWidth, 
            _currentDescription._mazeHeight, 
            _pathDisplayerCreator, 
            _carverCreator);
        _currentGridComposer.SetWinAction(() => _winMenu.Show()) ;
    }
    private void ResolveCarver(CarverAlgorithm algorithm)
    {
        switch (algorithm)
        {
            case CarverAlgorithm.BinaryTree:

                _carverCreator = (grid) => { return new BinaryTreeAlgorithm(grid); };
                break;
            case CarverAlgorithm.Sidewinder:
                _carverCreator = (grid) => { return new SidewinderAlgorithm(grid); };
                break;
            case CarverAlgorithm.AldousBroder:
                _carverCreator = (grid) => { return new AldousBroderAlgorithm(grid); };
                break;
            case CarverAlgorithm.Wilson:
                _carverCreator = (grid) => { return new WilsonsAlgorithm(grid); };
                break;
            case CarverAlgorithm.HuntAndKill:
                _carverCreator = (grid) => { return new HuntAndKillAlgorithm(grid); };
                break;
            case CarverAlgorithm.ReursiveBackTracker:
                _carverCreator = (grid) => { return new RecursiveBackTrackerAlgorithm(grid); };
                break;
            case CarverAlgorithm.Prim:
                _carverCreator = (grid) => { return new PrimsAlgorithm(grid); };
                break;
            case CarverAlgorithm.Eller:
                _carverCreator = (grid) => { return new EllersAlgorithm(grid); };
                break;
            case CarverAlgorithm.RecursiveDivision:
                _carverCreator = (grid) => { return new RecursiveDivisionAlgorithm(grid); };
                break;
        }
    }

    private void ResolveDifficulty(Difficulty level, CellType cellType)
    {
        switch (level)
        {
            case Difficulty.Easy:

                switch (cellType)
                {
                    case CellType.Rectangle:
                        _pathDisplayerCreator = (gridView) => new ConfusionPathDisplayer(gridView);
                        break;
                    case CellType.Triangle:
                        _pathDisplayerCreator = (gridView) => new TriangleConfusionPathDisplayer(gridView);
                        break;
                    case CellType.Hexagonal:
                        _pathDisplayerCreator = (gridView) => new HexConfusionPathDisplayer(gridView);
                        break;
                }
                break;
            case Difficulty.Normal:
                _pathDisplayerCreator = (gridView) => new NoPathDisplayer(gridView);
                break;
            case Difficulty.Walking:
                _pathDisplayerCreator = (gridView) => new PathDisplayer(gridView);
                break;
        }
    }

}