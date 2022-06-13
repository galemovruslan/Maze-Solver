using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleCellView : CellView
{
    public float Height
    {
        get
        {
            float northAngle = (180 - angle * 2f) * Mathf.Deg2Rad;
            float angleRad = Mathf.Deg2Rad * angle;
            float sideLength = _width * Mathf.Sin(angleRad) / Mathf.Sin(northAngle);
            return Mathf.Sin(angleRad) * sideLength;
        }
    }

    public bool Uprite { get => (_cell as TriangleCell).Upright;}

    [SerializeField] private Transform _cellVisual;

    private bool _uprite;
    private float _height;
    private readonly float angle = 55f;


    public override void Init(Cell cell)
    {
        base.Init(cell);
        var triangleCell = cell as TriangleCell;

        if (!Uprite)
        {
            SetUpsideDown();
        }
    }



    private void SetUpsideDown()
    {
        _cellVisual.Rotate(new Vector3(0, 180, 0));
    }

    protected override void CarveDirection(CellSide side)
    {
        if (Uprite)
        {
            switch (side)
            {
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
        else
        {
            switch (side)
            {
                case CellSide.North:
                    HideWall(_southWall);
                    break;
                case CellSide.West:
                    HideWall(_eastWall);
                    break;
                case CellSide.East:
                    HideWall(_westWall);
                    break;
            }
        }
    }


}
