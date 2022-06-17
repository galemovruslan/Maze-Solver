using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialGroupSelection : MonoBehaviour
{

    [SerializeField] private List<Toggle> _options;

    public int Value => _selectionValue;

    public event Action<int> ValueChange;

    private int _selectionValue;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        for (int i = 0; i < _options.Count; i++)
        {
            int index = i;

            _options[i].onValueChanged.AddListener(value =>
            {
                if (value)
                {
                    OnToggleChange(index);
                }
            });

            if (_options[i].isOn)
            {
                OnToggleChange(i);
            }
        }
    }

    private void OnToggleChange(int value)
    {
        _selectionValue = value;
        ValueChange?.Invoke(_selectionValue);
    }

}
