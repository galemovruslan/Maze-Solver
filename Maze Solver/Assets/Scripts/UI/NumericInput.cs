using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class NumericInput : MonoBehaviour
{
    public int Value => _currentValue;
    public event Action<int> ValueChange;


    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private int _maxValue;
    [SerializeField] private int _minValue;

    private int _currentValue;


    private void Awake()
    {
        _slider.maxValue = _maxValue;
        _slider.minValue = _minValue;
        _currentValue = ConstrainValue(_currentValue, _minValue, _maxValue);
        _inputField.text = _currentValue.ToString();
        _slider.value = _currentValue;
    }

    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnSliderChanged);
        _inputField.onEndEdit.AddListener(OnInputChanged);
    }

    private void OnSliderChanged(float value)
    {
        _currentValue = (int)value;
        _currentValue = ConstrainValue(_currentValue, _minValue, _maxValue);
        _inputField.text = _currentValue.ToString();
        ValueChange?.Invoke(_currentValue);
    }

    private void OnInputChanged(string value)
    {
        try
        {
            int intValue = int.Parse(value);
            _currentValue = ConstrainValue(intValue, _minValue, _maxValue);
            _slider.value = _currentValue;
            _inputField.text = _currentValue.ToString();
            ValueChange?.Invoke(_currentValue);
        }
        catch (FormatException e)
        {
            _inputField.text = _currentValue.ToString();
        }
    }

    private int ConstrainValue(int value, int min, int max)
    {
        return System.Math.Min(max, System.Math.Max(min, value));
    }
}
