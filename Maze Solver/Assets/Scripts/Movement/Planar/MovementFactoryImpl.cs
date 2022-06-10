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
        var jumpState = new StateJump(stateMachine, this, characterController, _moveParameters);
        var dashState = new StateDash(stateMachine, this, characterController, 7.5f, 0.1f);

        _map.Add(typeof(StateMoving), moveState);
        _map.Add(typeof(StateJump), jumpState);
        _map.Add(typeof(StateDash), dashState);
    }

}
public struct FactoryParameters
{
    public InputActions inputActions;
    public IMovementFSM stateMachine;
    public ICharacterMover characterController;
    public MoveParameters moveParameters;
}

