using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathCreatePolicy
{
    public Cell Start { get; }
    public Cell End { get; }

    void GetPathEndPoints();

}
