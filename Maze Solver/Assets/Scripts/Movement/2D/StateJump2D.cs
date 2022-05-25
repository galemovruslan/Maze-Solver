using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateJump2D : StateJump
{


    public StateJump2D(IMovementFSM stateMachine, IMovementStateFactory factory, ICharacterMover characterController,
        MoveParameters parameters)
        : base(stateMachine, factory, characterController, parameters)
    {
        AllowJump();
    }

    protected override IMovementState GetMoveState()
    {
        return _factory.Create<StateMove2D>();
    }

}
