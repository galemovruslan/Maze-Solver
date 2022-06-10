using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFactory2D : MovementFactoryImpl
{

    public MovementFactory2D(FactoryParameters parameters) : base(parameters)
    {
    }

    protected override void PopulateMap(IMovementFSM stateMachine, ICharacterMover characterController)
    {
        var moveState = new StateMove2D(stateMachine, this, characterController, _moveParameters);
        var jumpState = new StateJump2D(stateMachine, this, characterController, _moveParameters);

        _map.Add(typeof(StateMove2D), moveState);
        _map.Add(typeof(StateJump2D), jumpState);
    }

}
