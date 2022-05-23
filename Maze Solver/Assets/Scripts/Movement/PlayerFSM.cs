using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : IMovementFSM
{
    public event Action<string> OnStateChange;

    private IMovementState _currentState;
    private bool _initialized = false;

    public PlayerFSM()
    { 
    }

    public void Init(IMovementState firstState)
    {
        _currentState = firstState;
        _initialized = true;
    }

    public void ChangeState(IMovementState newState)
    {
        CheckInitialized();
        _currentState = newState;
        string stateName = _currentState.StateName;
        OnStateChange?.Invoke(stateName);
    }

    public void Tick()
    {
        CheckInitialized();
        _currentState.Move();
    }

    private void CheckInitialized()
    {
        if (!_initialized)
        {
            throw new System.Exception("FSM has not been initialized");
        }
    }
}
