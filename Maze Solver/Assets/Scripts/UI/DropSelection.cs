using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Dropdown))]
public class DropSelection : MonoBehaviour
{ 
    public int Value => _currentSelection;

    public event Action<int> ValueChange;

    private TMP_Dropdown _dropdown;
    private int _currentSelection;

    private void Awake()
    {
        _dropdown = GetComponent<TMP_Dropdown>();
        _dropdown.onValueChanged.AddListener(OnSelection);
    }

    private void OnSelection(int itemIndex)
    {
        _currentSelection = itemIndex;
        ValueChange?.Invoke(_currentSelection);
    }
}
