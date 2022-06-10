using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMove2D : StateMoving
{
    public StateMove2D(IMovementFSM stateMachine, IMovementStateFactory factory, ICharacterMover characterController, MoveParameters parameters)
        : base(stateMachine, factory, characterController, parameters)
    {
    }

    public override void Move(Vector2 inputDirection, bool jumpCommand)
    {
        inputDirection.y = 0;
        base.Move(inputDirection, jumpCommand);
    }

    protected override IMovementState GetJumpState()
    {
        return _factory.Create<StateJump2D>();
    }
}
