using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFactoryImpl : IMovementStateFactory
{
    protected InputActions _inputActions;
    protected Dictionary<Type, IMovementState> _map = new Dictionary<Type, IMovementState>();
    protected MoveParameters _moveParameters;

    public MovementFactoryImpl(FactoryParameters parameters)
    {
        _inputActions = parameters.inputActions;
        _moveParameters = parameters.moveParameters;
        PopulateMap(parameters.stateMachine, parameters.characterController);
    }
    public IMovementState Create<T>() where T : IMovementState
    {
        return _map[typeof(T)];
    }

    protected virtual void PopulateMap(IMovementFSM stateMachine, ICharacterMover characterController)
    {
        var moveState = new StateMoving(stateMachine, this, characterController, _moveParameters);
        RouteInput(moveState, _inputActions);

        var jumpState = new StateJump(stateMachine, this, characterController, _moveParameters);
        RouteInput(jumpState, _inputActions);

        _map.Add(typeof(StateMoving), moveState);
        _map.Add(typeof(StateJump), jumpState);
    }

    protected void RouteInput(IMovementState state, InputActions inputActions)
    {
        inputActions.Player.Movement.performed += state.HandleMovement;
        inputActions.Player.Movement.canceled += state.HandleMovement;
        inputActions.Player.Jump.started += state.HandleJump;
        inputActions.Player.Jump.canceled += state.HandleJump;
        inputActions.Player.Sprint.performed += state.HandleSprint;
        inputActions.Player.Sprint.canceled += state.HandleSprint;
    }
}
public struct FactoryParameters
{
    public InputActions inputActions;
    public IMovementFSM stateMachine;
    public ICharacterMover characterController;
    public MoveParameters moveParameters;

}

