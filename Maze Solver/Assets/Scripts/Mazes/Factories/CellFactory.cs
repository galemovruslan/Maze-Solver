using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellFactory : ICellFactory
{
    private CellView _prefab;

    public CellFactory(CellView prefab)
    {
        _prefab = prefab;
    }

    public CellView Spawn(Vector3 position)
    {
        CellView newCell = GameObject.Instantiate(_prefab, position, Quaternion.identity);
        return newCell;
    }

}
