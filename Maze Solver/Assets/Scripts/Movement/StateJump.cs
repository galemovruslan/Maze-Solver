using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateJump : IMovementState
{
    private IMovementFSM _movementFSM;
    private IMovementStateFactory _factory;
    private Vector3 _currentVelocity;
    CharacterController _characterController;
    private Vector2 _inputDirection;

    private bool _wasGroundedLastFrame = true;
    private bool _jumpCommand;
    private bool _jumpAllowed = false;
    private bool _jumpThisFrame = false;
    private bool _isFalling = false;

    private float _jumpForce;
    private float _moveSpeed = 1f;

    private readonly float _gravityConstant = -9.8f;
    private readonly float _fallAcceleration = 2f;

    public StateJump(IMovementFSM stateMachine, IMovementStateFactory factory, CharacterController characterController, float jumpHeight)
    {
        _movementFSM = stateMachine;
        _factory = factory;
        _characterController = characterController;
        _jumpForce = jumpHeight;
    }

    public void Init(Vector3 velocity)
    {
        _currentVelocity = velocity;
        Debug.Log(this.ToString());
    }

    public void HandleMovement(InputAction.CallbackContext context)
    {
        _inputDirection = context.ReadValue<Vector2>();
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        _jumpCommand = context.ReadValueAsButton();
    }


    public void Move()
    {
        _isFalling = !_characterController.isGrounded && _currentVelocity.y < 0;

        HandleGrounging(
            isGrounded: _characterController.isGrounded,
            isMoveDown: _currentVelocity.y < 0);

        HandleMovement(_characterController);

        _wasGroundedLastFrame = _characterController.isGrounded;
    }

    private void HandleMovement(CharacterController characterController)
    {
        _currentVelocity = CalculateMoveInput(_currentVelocity, _inputDirection);
        characterController.Move(_currentVelocity * Time.deltaTime);
        _currentVelocity = CalculateMoveGravity(_currentVelocity, _gravityConstant);
        _currentVelocity = CalculateMoveJump(_currentVelocity, _jumpCommand, _jumpForce);
    }

    private Vector3 CalculateMoveJump(Vector3 currentVelocity, bool jumpCommand, float jumpForce)
    {
        bool jumpThisFrame = _jumpAllowed && jumpCommand;

        if (jumpThisFrame)
        {
            currentVelocity.y = jumpForce;
        }
        return currentVelocity;
    }

    private Vector3 CalculateMoveInput(Vector3 currentVelocity, Vector2 inputDirection)
    {
        currentVelocity.x = inputDirection.x * _moveSpeed;
        currentVelocity.z = inputDirection.y * _moveSpeed;
        return currentVelocity;
    }

    private Vector3 CalculateMoveGravity(Vector3 currentVelocity, float gravityConstant)
    {
        if (_characterController.isGrounded)
        {
            currentVelocity.y = gravityConstant;
        }
        else
        {
            currentVelocity.y += gravityConstant * Time.deltaTime;
        }
        return currentVelocity;
    }

    private void HandleGrounging(bool isGrounded, bool isMoveDown)
    {
        if (!isGrounded || !isMoveDown)
        {
            return;
        }

        var nextState = _factory.Create<StateMoving>();
        ChangeState(nextState);
    }

    private void ChangeState(IMovementState nextState)
    {
        nextState.Init(_currentVelocity);
        _movementFSM.ChangeState(nextState);
    }

}
