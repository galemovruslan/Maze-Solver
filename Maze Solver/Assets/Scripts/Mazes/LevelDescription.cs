using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDescription 
{
    private int _mazeWidth;
    private int _mazeHeight;
    private CellType _cellType;
    private CarverAlgorithm _carverAlgorithm;
    private Difficulty _difficulty;


    public LevelDescription(int mazeWidth, int mazeHeight, CellType cellType, CarverAlgorithm carverAlgorithm, Difficulty difficulty)
    {
        _mazeWidth = mazeWidth;
        _mazeHeight = mazeHeight;
        _cellType = cellType;
        _carverAlgorithm = carverAlgorithm;
        _difficulty = difficulty;
    }
}
