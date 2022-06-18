using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoPathDisplayer : PathDisplayer
{
    public NoPathDisplayer(GridView gridView) : base(gridView)
    {
    }

    public override void DisplayHint(List<Cell> path)
    {
    }
}
