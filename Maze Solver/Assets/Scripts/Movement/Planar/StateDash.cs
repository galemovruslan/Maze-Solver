using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateDash : IMovementState
{
    private ICharacterMover _characterController;
    private IMovementFSM _movementFSM;
    private Vector3 _currentVelocity;
    private Vector3 _initVelocity;
    private float _distance;
    private float _time;
    private float _speed;
    private Timer _timer;

    protected IMovementStateFactory _factory;

    public StateDash(IMovementFSM stateMachine, IMovementStateFactory factory, ICharacterMover characterController, float distance, float time)
    {
        _distance = distance;
        _time = time;
        _speed = _distance / _time;

        _characterController = characterController;
        _movementFSM = stateMachine;
        _factory = factory;

        _timer = new Timer(_time);
        _timer.OnDone += Timer_OnDone;
    }

    public string StateName => MovementNames.DashName;

    public void Init(Vector3 velocity)
    {
        var moveDirection = velocity;
        moveDirection.y = 0;
        _currentVelocity = moveDirection.normalized * _speed;
        _initVelocity = velocity;
        _timer.Reset();
        _timer.Start();
    }

    public void Move(Vector2 inputDirection, bool jumpCommand)
    {
        _timer.Tick(Time.deltaTime);
        _characterController.Move(_currentVelocity*Time.deltaTime);
    }

    private void Timer_OnDone()
    {
        IMovementState nextState;
        if (_characterController.isGrounded)
        {
             nextState = _factory.Create<StateMoving>(); 
        }
        else
        {
            nextState = _factory.Create<StateJump>();
        }
        ChangeState(nextState);
    }

    public void ChangeState(IMovementState nextState)
    {
        nextState.Init(_initVelocity);
        _movementFSM.ChangeState(nextState);
    }
}
