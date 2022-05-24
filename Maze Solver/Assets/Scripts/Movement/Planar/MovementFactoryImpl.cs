using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFactoryImpl : IMovementStateFactory
{
    private InputActions _inputActions;
    private Dictionary<Type, IMovementState> _map = new Dictionary<Type, IMovementState>();
    private MoveParameters _moveParameters;

    public MovementFactoryImpl(InputActions inputActions, IMovementFSM stateMachine, ICharacterMover characterController, MoveParameters moveParameters)
    {
        _inputActions = inputActions;
        _moveParameters = moveParameters;
        PopulateMap(stateMachine, characterController);
    }
    public IMovementState Create<T>() where T : IMovementState
    {
        return _map[typeof(T)];
    }

    private void PopulateMap(IMovementFSM stateMachine, ICharacterMover characterController)
    {
        var moveState = new StateMoving(stateMachine, this, characterController, _moveParameters.MoveSpeed, _moveParameters.SprintSpeed, _moveParameters.JumpHeight, _moveParameters.JumpTime);
        RouteInput(moveState, _inputActions);

        var jumpState = new StateJump(stateMachine, this, characterController, _moveParameters.JumpHeight, _moveParameters.JumpTime);
        RouteInput(jumpState, _inputActions);

        _map.Add(typeof(StateMoving), moveState);
        _map.Add(typeof(StateJump), jumpState);
    }

    private void RouteInput(IMovementState state, InputActions inputActions)
    {
        inputActions.Player.Movement.performed += state.HandleMovement;
        inputActions.Player.Movement.canceled += state.HandleMovement;
        inputActions.Player.Jump.performed += state.HandleJump;
        inputActions.Player.Jump.canceled += state.HandleJump;
        inputActions.Player.Sprint.performed += state.HandleSprint;
        inputActions.Player.Sprint.canceled += state.HandleSprint;
    }   
}
