using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellView : MonoBehaviour
{
    public float Width  => _width;

    [SerializeField] private GameObject _northWall;
    [SerializeField] private GameObject _southWall;
    [SerializeField] private GameObject _eastWall;
    [SerializeField] private GameObject _westWall;
    [SerializeField] private float _width;

    private Cell _cell;

    public void Init(Cell cell)
    {
        _cell = cell;
        ProcessLinks();
    }

    private void ProcessLinks()
    {
        foreach (var direction in _cell.LinkDirections())
        {
            CarveWall(direction);
        }
    }

    private void CarveWall(CellSide side)
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

    
}
