using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFactoryImpl : IMovementStateFactory
{
    private InputActions _inputActions;
    private Dictionary<Type, IMovementState> _map = new Dictionary<Type, IMovementState>();

    public MovementFactoryImpl(InputActions inputActions, IMovementFSM stateMachine, CharacterController characterController)
    {
        _inputActions = inputActions;
        PopulateMap(stateMachine, characterController);
    }

    private void PopulateMap(IMovementFSM stateMachine, CharacterController characterController)
    {
        var moveState = new StateMoving(stateMachine, this, characterController, 2f);
        RouteInput(moveState, _inputActions);

        var jumpState = new StateJump(stateMachine, this, characterController, 100f );
        RouteInput(jumpState, _inputActions);

        _map.Add(typeof(StateMoving), moveState);
        _map.Add(typeof(StateJump), jumpState);
    }

    void RouteInput(IMovementState state, InputActions inputActions)
    {
        inputActions.Player.Movement.performed += state.HandleMovement;
        inputActions.Player.Movement.canceled += state.HandleMovement;
        inputActions.Player.Jump.performed += state.HandleJump;
        inputActions.Player.Jump.canceled += state.HandleJump;
    }

    public IMovementState Create<T>() where T : IMovementState
    {
        return _map[typeof(T)];
    }
}
