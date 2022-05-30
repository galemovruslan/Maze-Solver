using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFactory
{
    CellView _prefab;
    Transform _parent;

    public CellFactory(CellView prefab, Transform Parent)
    {
        _prefab = prefab;
    }

    public CellView Spawn(Vector3 position)
    {
        CellView newCell = GameObject.Instantiate(_prefab, position, Quaternion.identity);
        return newCell;
    }

}