using UnityEngine;

public interface ICellFactory
{
    CellView Spawn(Vector3 position);
}