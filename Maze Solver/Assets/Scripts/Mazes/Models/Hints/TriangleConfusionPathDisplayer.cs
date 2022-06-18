using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleConfusionPathDisplayer : ConfusionPathDisplayer
{
    public TriangleConfusionPathDisplayer(GridView gridView) : base(gridView)
    {
    }

    protected override Vector3 GetRandomDirection(int startRow, int startCol)
    {
        return base.GetRandomDirection(startRow, startCol);
    }
}
