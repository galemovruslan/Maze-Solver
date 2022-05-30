using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellView : MonoBehaviour
{
    private enum CellPathType
    {
        Normal,
        Start,
        Goal
    }
    public float Width  => _width;

    [SerializeField] private GameObject _northWall;
    [SerializeField] private GameObject _southWall;
    [SerializeField] private GameObject _eastWall;
    [SerializeField] private GameObject _westWall;
    [SerializeField] private float _width;
    [SerializeField] private GameObject _centerPoint;
    [Space]
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _goalColor;
    [SerializeField] private ColorChanger _colorChanger;

    private Cell _cell;
    CellPathType _cellType;

    public void Init(Cell cell)
    {
        _cell = cell;
        OpenPathToLinks();
    }

    public void SetStart()
    {
        _cellType = CellPathType.Start;
        _colorChanger.ChangeColor(_startColor);
    }

    public void SetGoal()
    {
        _cellType = CellPathType.Goal;
        _colorChanger.ChangeColor(_goalColor);
    }

    private void OpenPathToLinks()
    {
        foreach (var direction in _cell.LinkDirections())
        {
            CarveDirection(direction);
        }
    }

    private void CarveDirection(CellSide side)
    {
        switch (side)
        {
            case CellSide.North:
                HideWall(_northWall);
                break;
            case CellSide.South:
                HideWall(_southWall);
                break;
            case CellSide.East:
                HideWall(_eastWall);
                break;
            case CellSide.West:
                HideWall(_westWall);
                break;
        }
    }

    private void HideWall(GameObject wall)
    {
        wall.SetActive(false);
    }

    public void ShowPoint(bool isShow)
    {
        _centerPoint.SetActive(isShow);
    }
    
}
