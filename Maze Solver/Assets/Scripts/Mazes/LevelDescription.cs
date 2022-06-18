using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelDescription
{
    public int _mazeWidth;
    public int _mazeHeight;
    public CellType _cellType;
    public CarverAlgorithm _carverAlgorithm;
    public Difficulty _difficulty;


    public LevelDescription(int mazeWidth, int mazeHeight, CellType cellType, CarverAlgorithm carverAlgorithm, Difficulty difficulty)
    {
        _mazeWidth = mazeWidth;
        _mazeHeight = mazeHeight;
        _cellType = cellType;
        _carverAlgorithm = carverAlgorithm;
        _difficulty = difficulty;
    }

}
