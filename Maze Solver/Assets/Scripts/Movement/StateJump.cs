using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateJump : IMovementState
{
    private IMovementFSM _movementFSM;
    private IMovementStateFactory _factory;
    private Vector3 _velocity;
    CharacterController _characterController;

    private bool _jumpCommand;
    private bool _jumpAllowed = true;
    private bool _jumpThisFrame = false;
    private float _jumpForce;
    private readonly float _gravityConstant = -9.8f;

    public StateJump(IMovementFSM stateMachine, IMovementStateFactory factory, CharacterController characterController, float jumpHeight)
    {
        _movementFSM = stateMachine;
        _factory = factory;
        _characterController = characterController;
        _jumpForce = jumpHeight;
    }

    public void Init()
    {
        
    }

    public void HandleMovement(InputAction.CallbackContext context)
    {

    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        _jumpCommand = context.ReadValueAsButton();
    }


    public void Move()
    {
        if (_characterController.isGrounded)
        {
            HandleGrounging();
        }
        HandleMovement(_characterController);
    }

    private void HandleMovement(CharacterController characterController)
    {
        var intputMove = CalculateMoveFromInput();
        var gravityMove = CalculateMoveFromGravity();

        Vector3 totalVelocity;
        bool jumpThisFrame = _jumpAllowed && _jumpCommand;

        if (jumpThisFrame)
        {
            _velocity.y = 0;
            totalVelocity = _velocity + intputMove;
        }
        else
        {
            totalVelocity = _velocity + gravityMove;
        }

        characterController.Move(totalVelocity);
        _velocity = totalVelocity;
        Debug.Log("Jump velocity" + _velocity.ToString());
    }

    private Vector3 CalculateMoveFromInput()
    {
        float jumpForce = _jumpCommand ? _jumpForce : 0;
        return new Vector3(0, jumpForce, 0) * Time.deltaTime;
    }
    private Vector3 CalculateMoveFromGravity()
    {
        return new Vector3(0, _gravityConstant, 0) * Time.deltaTime;
    }

    private void HandleGrounging()
    {
        var nextState = _factory.Create<StateMoving>();
        nextState.Init();

        _movementFSM.ChangeState(nextState);
    }

}
