using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSprint : StateMoving
{
    public override string StateName => MovementNames.DashName;
    public float Duration => _duration; 

    private Timer _sprintTime;
    private float _duration;


    public StateSprint(IMovementFSM stateMachine, IMovementStateFactory factory, ICharacterMover characterController, MoveParameters parameters)
        : base(stateMachine, factory, characterController, parameters)
    {
        _movingSpeed = parameters.SprintSpeed;
        _duration = parameters.SprintTime;
        _sprintTime = new Timer(_duration);
        _sprintTime.OnDone += _sprintTime_OnDone;
    }

   
    public override void Init(Vector3 velocity)
    {
        base.Init(velocity);
        _sprintTime.Reset();
        _sprintTime.Start();
    }

    protected override void CheckStateChange()
    {
        base.CheckStateChange();

        if (_currentVelocity.x == 0 && _currentVelocity.z == 0)
        {
            IMovementState nextState = GetMoveState();
            ChangeState(nextState);
            _sprintTime.Reset();
        }
    }

    private void _sprintTime_OnDone()
    {
        IMovementState nextState = GetMoveState();
        ChangeState(nextState);
    }

    public override void ChangeState(IMovementState nextState)
    {
        base.ChangeState(nextState);
    }

    private IMovementState GetMoveState()
    {
        return _factory.Create<StateMoving>();
    }
}
