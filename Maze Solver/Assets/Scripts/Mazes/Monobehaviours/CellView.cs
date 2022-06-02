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

    [SerializeField] protected GameObject _northWall;
    [SerializeField] protected GameObject _southWall;
    [SerializeField] protected GameObject _eastWall;
    [SerializeField] protected GameObject _westWall;
    [SerializeField] protected float _width;
    [SerializeField] private GameObject _hitnMarker;
    [Space]
    [SerializeField] private Color _startColor;
    [SerializeField] private Color _goalColor;
    [SerializeField] private ColorChanger _colorChanger;

    private Cell _cell;
    CellPathType _cellType;

    public virtual void Init(Cell cell)
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

    protected virtual void OpenPathToLinks()
    {
        foreach (var direction in _cell.LinkDirections())
        {
            CarveDirection(direction);
        }
    }

    protected virtual void CarveDirection(CellSide side)
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

    protected void HideWall(GameObject wall)
    {
        wall.SetActive(false);
    }

    public void ShowHint(bool isShow, Vector3 direction)
    {
        _hitnMarker.SetActive(isShow);
        _hitnMarker.transform.LookAt(_hitnMarker.transform.position + direction);
    }
    
}
