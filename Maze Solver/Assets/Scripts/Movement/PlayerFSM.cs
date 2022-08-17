using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : IMovementFSM
{
    public event Action<string> OnStateChange;
    public IMovementState CurrentState => _currentState;

    private IMovementState _currentState;
    private bool _initialized = false;

    private Vector2 _inputDirection;
    private bool _jumpCommand;

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
        _currentState.Move(_inputDirection, _jumpCommand);
    }
    public void ForceState(IMovementState newState)
    {
        _currentState.ChangeState(newState);
    }

    private void CheckInitialized()
    {
        if (!_initialized)
        {
            throw new System.Exception("FSM has not been initialized");
        }
    }

    public void HandleMovement(Vector2 inputDirection)
    {
        _inputDirection = inputDirection;
    }

    public void HandleJump(bool jumpCommand)
    {
        _jumpCommand = jumpCommand;
    }
}
