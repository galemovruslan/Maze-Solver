using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeSettings : MonoBehaviour
{
    [SerializeField] private NumericInput _widthInput;
    [SerializeField] private NumericInput _heghtInput;
    [SerializeField] private RadialGroupSelection _cellTypeSelector;
    [SerializeField] private DropSelection _algorithmSelector;
    [SerializeField] private RadialGroupSelection _difficultySelector;
    [SerializeField] private IndicatorPanel _featurePanel;
    [SerializeField] private Button _acceptButton;

    private int _mazeWidth;
    private int _mazeHeight;
    private CellType _cellType;
    private CarverAlgorithm _carverAlgorithm;
    private Difficulty _difficulty;

    Dictionary<int, CellType> _cellTypeMap = new Dictionary<int, CellType>()
    {
        { 0, CellType.Triangle },
        {1, CellType.Rectangle },
        {2, CellType.Hexagonal }
    };

    Dictionary<int, CarverAlgorithm> _algorithmMap = new Dictionary<int, CarverAlgorithm>()
    {
        {0, CarverAlgorithm.BinaryTree},
        {1, CarverAlgorithm.Sidewinder },
        {2, CarverAlgorithm.AldousBroder },
        {3, CarverAlgorithm.HuntAndKill },
        {4, CarverAlgorithm.ReursiveBackTracker },
        {5, CarverAlgorithm.Prim },
        {6, CarverAlgorithm.Eller },
        {7, CarverAlgorithm.Wilson },
        {8, CarverAlgorithm.RecursiveDivision }
    };

    Dictionary<int, Difficulty> _difficultyMap = new Dictionary<int, Difficulty>()
    {
        {0, Difficulty.Normal },
        {1, Difficulty.Easy },
        {2, Difficulty.Walking }
    };

    private void OnEnable()
    {
        _widthInput.ValueChange += OnWidthChange;
        _heghtInput.ValueChange += OnHeightChange;
        _cellTypeSelector.ValueChange += OnCellTypeChange;
        _algorithmSelector.ValueChange += OnAlgorithmChange;
        _difficultySelector.ValueChange += OnDifficultyChange;
        _acceptButton.onClick.AddListener(OnAccept);
    }

    private void OnDisable()
    {
        _widthInput.ValueChange -= OnWidthChange;
        _heghtInput.ValueChange -= OnHeightChange;
        _cellTypeSelector.ValueChange -= OnCellTypeChange;
        _algorithmSelector.ValueChange -= OnAlgorithmChange;
        _difficultySelector.ValueChange -= OnDifficultyChange;
        _acceptButton.onClick.RemoveListener(OnAccept);
    }

    private void Start()
    {
        OnDifficultyChange(_difficultySelector.Value);
    }

    private void OnWidthChange(int value)
    {
        _mazeWidth = value;
    }

    private void OnHeightChange(int value)
    {
        _mazeHeight = value;
    }

    private void OnCellTypeChange(int value)
    {
        _cellType = _cellTypeMap[value];

    }

    private void OnAlgorithmChange(int value)
    {
        _carverAlgorithm = _algorithmMap[value];
    }

    private void OnDifficultyChange(int value)
    {
        _difficulty = _difficultyMap[value];
        LightPanel();
    }

    private void LightPanel()
    {
        switch (_difficulty)
        {
            case Difficulty.Easy:
                {
                    _featurePanel.TurnOnAt(0);
                    _featurePanel.TurnOffAt(1);
                    _featurePanel.TurnOnAt(2);
                    break;
                }
            case Difficulty.Normal:
                {
                    _featurePanel.TurnOffAt(0);
                    _featurePanel.TurnOffAt(1);
                    _featurePanel.TurnOffAt(2);
                    break;
                }
            case Difficulty.Walking:
                {
                    _featurePanel.TurnOnAt(0);
                    _featurePanel.TurnOnAt(1);
                    _featurePanel.TurnOnAt(2);
                    break;
                }
        }
    }

    private void OnAccept()
    {
        LevelDescription levelDescription = new LevelDescription(_mazeWidth, _mazeHeight, _cellType, _carverAlgorithm, _difficulty);
    }

}