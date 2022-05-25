using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    public bool IsDone => _isDone;
    public event Action OnDone;

    private float _time;
    private float _setTime;
    private bool _isRunning = false;
    private bool _isDone = false;

    public Timer(float setTime)
    {
        _setTime = setTime;
    }

    public void Start()
    {
        _isRunning = true;
    }

    public void Reset()
    {
        _time = 0;
        _isRunning = false;
        _isDone = false;
    }

    public void Tick(float deltaTime)
    {
        if (!_isRunning)
        {
            return;
        }

        _time += deltaTime;

        if(_time >= _setTime)
        {
            _isRunning = false;
            _isDone = true;
            OnDone?.Invoke();
        }
    }

}
