using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicator : MonoBehaviour
{
    [SerializeField] private Color _offColor;
    [SerializeField] private Color _onColor;
    [SerializeField] private Image _image;


    public void TurnOn()
    {
        _image.color = _onColor;
    }

    public void TurnOff()
    {
        _image.color = _offColor;
    }
}
