using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    
    public static Cell SampleCell(List<Cell> list)
    {
        int idx;
        Cell cell;
        do
        {
            idx = Random.Range(0, list.Count);
            cell = list[idx];
        } while (cell == null);
        return cell;
    }

}
