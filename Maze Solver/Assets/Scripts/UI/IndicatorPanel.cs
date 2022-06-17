using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorPanel : MonoBehaviour
{
    [SerializeField] private List<Indicator> _indicators;

    public void TurnOnAt(int index)
    {
        _indicators[index].TurnOn();
    }

    public void TurnOffAt(int index)
    {
        _indicators[index].TurnOff();
    }

}
