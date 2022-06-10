using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler
{
    private InputActions _inputActions;
    private IMovementFSM _movementFSM;
    private AbilityHandler _abilityHandler;

    private Vector2 _inputDirection;
    private bool _jumpCommand;

    public InputHandler(InputActions inputActions, IMovementFSM movementFSM, AbilityHandler actionHandler)
    {
        _inputActions = inputActions;
        _movementFSM = movementFSM;
        _abilityHandler = actionHandler;

        RouteInput(_inputActions);
    }

    protected void RouteInput(InputActions inputActions)
    {
        inputActions.Player.Movement.performed += HandleMovement;
        inputActions.Player.Movement.canceled += HandleMovement;
        inputActions.Player.Jump.started += HandleJump;
        inputActions.Player.Jump.canceled += HandleJump;
        inputActions.Player.Sprint.performed += HandleSprint;
        inputActions.Player.Sprint.canceled += HandleSprint;
        inputActions.Player.Ability.started += HandleAbility;
    }

    private void HandleAbility(InputAction.CallbackContext context)
    {
        _abilityHandler.TryUse();
    }

    private void HandleSprint(InputAction.CallbackContext context)
    {

    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        _jumpCommand = context.ReadValueAsButton();
        _movementFSM.HandleJump(_jumpCommand);
    }

    private void HandleMovement(InputAction.CallbackContext context)
    {
        _inputDirection = context.ReadValue<Vector2>();
        _movementFSM.HandleMovement(_inputDirection);
    }
}
