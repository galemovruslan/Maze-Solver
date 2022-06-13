using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCellView : CellView
{
    public float NorthToSouth { get => BSize * 2f; }
    public float EastToWest { get => _width * 2f; }

    public float ASize { get => _width / 2f; }
    public float BSize { get => _width * Mathf.Sqrt(3) / 2f; }

    [SerializeField] protected GameObject _northWestWall;
    [SerializeField] protected GameObject _northEastWall;
    [SerializeField] protected GameObject _southWestWall;
    [SerializeField] protected GameObject _southEastWall;

    protected override void CarveDirection(CellSide side)
    {
        switch (side)
        {
            case CellSide.NorthEast:
                HideWall(_northEastWall);
                break;
            case CellSide.NorthWest:
                HideWall(_northWestWall);
                break;
            case CellSide.SouthEast:
                HideWall(_southEastWall);
                break;
            case CellSide.SouthWest:
                HideWall(_southWestWall);
                break;
        }

        base.CarveDirection(side);
    }

}
