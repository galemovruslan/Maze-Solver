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

    public override void HandleMovement(InputAction.CallbackContext context)
    {
        _inputDirection = context.ReadValue<Vector2>();
        _inputDirection.y = 0;
    }

    protected override IMovementState GetJumpState()
    {
        return _factory.Create<StateJump2D>();
    }
}
