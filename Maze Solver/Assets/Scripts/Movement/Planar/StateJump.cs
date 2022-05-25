using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateJump : IMovementState
{
    public string StateName => MovementNames.JumpName;

    private IMovementFSM _movementFSM;
    private Vector3 _currentVelocity;
    private Vector3 _inertialVelocity;
    private Timer _timer;

    protected IMovementStateFactory _factory;
    protected ICharacterMover _characterController;
    protected Vector2 _inputDirection;
    protected bool _jumpCommand;

    private bool _onCoolDown = true;
    private bool _wasGroundedLastFrame = true;
    private bool _jumpAllowed = false;
    private bool _isFalling = false;

    private float _moveSpeed = 1f;
    private float _jumpInitialMoveSpeed = 1f;
    private float _gravity = -9.8f;
    private float _jumpHeight;
    private float _jumpDuration;
    private float _jumpForce;

    private readonly float _fallGravityMultiplyer = 1.5f;

    public StateJump(IMovementFSM stateMachine, IMovementStateFactory factory, ICharacterMover characterController, MoveParameters parameters)
    {
        _movementFSM = stateMachine;
        _factory = factory;
        _characterController = characterController;

        _jumpInitialMoveSpeed = parameters.MoveSpeed;
        _jumpHeight = parameters.JumpHeight;
        _jumpDuration = parameters.JumpTime;

        _timer = new Timer(0.4f);
        _timer.OnDone += _timer_OnDone;
        SetupJumpValues();
    }

    public void Init(Vector3 velocity)
    {
        _currentVelocity = velocity;
        _inertialVelocity = velocity;

        _jumpAllowed = true;
        _onCoolDown = true;
        _timer.Reset();
        _timer.Start();
        Debug.Log(this.ToString());
    }

    public virtual void HandleMovement(InputAction.CallbackContext context)
    {
        _inputDirection = context.ReadValue<Vector2>();
    }

    public virtual void HandleJump(InputAction.CallbackContext context)
    {
        _jumpCommand = context.ReadValueAsButton();
    }

    public virtual void HandleSprint(InputAction.CallbackContext context)
    {
    }

    public void Move()
    {
        _timer.Tick(Time.deltaTime);
        HandleGrounging(
            isGrounded: _characterController.isGrounded,
            isMoveDown: _currentVelocity.y < 0);

        HandleMovement(_characterController);

        _wasGroundedLastFrame = _characterController.isGrounded;
        _jumpCommand = false;
    }

    public void AllowJump()
    {
        _jumpAllowed = true;
    }

    public void DisableJump()
    {
        _jumpAllowed = false;
    }

    protected virtual bool CheckJumpAllowed()
    {
        return _jumpAllowed && !_onCoolDown;
    }

    private void HandleMovement(ICharacterMover characterController)
    {
        _isFalling = !_characterController.isGrounded && _currentVelocity.y < 0;
        _currentVelocity = CalculateMoveInput(_currentVelocity, _inputDirection);
        characterController.Move(_currentVelocity * Time.deltaTime);
        _currentVelocity = CalculateMoveGravity(_currentVelocity, _gravity, _fallGravityMultiplyer, _isFalling);
        bool jumpAllowed = CheckJumpAllowed();
        _currentVelocity = CalculateMoveJump(_currentVelocity, _jumpCommand, jumpAllowed, _jumpForce);
    }

    protected virtual Vector3 CalculateMoveJump(Vector3 currentVelocity, bool jumpCommand, bool jumpAllowed, float jumpForce)
    {
        bool jumpThisFrame = jumpAllowed && jumpCommand;

        if (jumpThisFrame)
        {
            currentVelocity.y = jumpForce;
            var newInertialVelocity = new Vector3(_inputDirection.x * _jumpInitialMoveSpeed, 0, _inputDirection.y * _jumpInitialMoveSpeed);
            SetInertialVelocity(newInertialVelocity);
            DisableJump();
        }
        return currentVelocity;
    }

    private Vector3 CalculateMoveInput(Vector3 currentVelocity, Vector2 inputDirection)
    {
        currentVelocity.x = _inertialVelocity.x + inputDirection.x * _moveSpeed;
        currentVelocity.z = _inertialVelocity.z + inputDirection.y * _moveSpeed;
        return currentVelocity;
    }

    protected virtual Vector3 CalculateMoveGravity(Vector3 currentVelocity, float gravityConstant, float fallMult, bool isFalling)
    {

        if (isFalling)
        {
            gravityConstant *= fallMult;
        }
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

        var nextState = GetMoveState();
        ChangeState(nextState);
    }

    private void ChangeState(IMovementState nextState)
    {
        nextState.Init(_currentVelocity);
        _movementFSM.ChangeState(nextState);
    }

    private void SetupJumpValues()
    {
        float timeToApex = _jumpDuration / 2f;
        _gravity = -(2 * _jumpHeight) / Mathf.Pow(timeToApex, 2);
        _jumpForce = (2 * _jumpHeight) / timeToApex;
    }

    protected virtual IMovementState GetMoveState()
    {
        return _factory.Create<StateMoving>();
    }

    private void _timer_OnDone()
    {
        _onCoolDown = false;
    }

    private void SetInertialVelocity(Vector3 value)
    {
        _inertialVelocity = value;
    }

}
