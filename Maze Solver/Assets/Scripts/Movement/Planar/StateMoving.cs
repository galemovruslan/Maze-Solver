using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMoving : IMovementState
{
    public string StateName => MovementNames.MoveName;

    private ICharacterMover _characterController;
    private IMovementFSM _movementFSM;
    private Vector3 _currentVelocity;
    private float _movingSpeed;
    private float _runningSpeed;
    private float _sprintingSpeed;
    private float _jumpForce = 10f;
    private float _jumpHeight;
    private float _jumpDuration;
    private bool _jumpThisFrame = false;

    protected IMovementStateFactory _factory;
    protected Vector2 _inputDirection;
    protected bool _jumpCommand = false;
    private readonly float _gravityConstant = -0.5f;

    public StateMoving(IMovementFSM stateMachine,
        IMovementStateFactory factory,
        ICharacterMover characterController,
        MoveParameters parameters)
    {
        _movementFSM = stateMachine;
        _factory = factory;
        _characterController = characterController;
        _runningSpeed = parameters.MoveSpeed;
        _sprintingSpeed = parameters.SprintSpeed;
        _jumpHeight = parameters.JumpHeight;
        _jumpDuration = parameters.JumpTime;

        _movingSpeed = _runningSpeed;
        SetupJumpValues();        
    }
    public void Init(Vector3 velocity)
    {
        _jumpThisFrame = false;
        _currentVelocity = velocity;
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
        _movingSpeed = context.ReadValueAsButton() ? _sprintingSpeed : _runningSpeed;
    }

    public void ChangeState(IMovementState nextState)
    {
        nextState.Init(_currentVelocity);
        _movementFSM.ChangeState(nextState);
    }

    public void Move()
    {
        _currentVelocity = CalculateMoveInput(_currentVelocity, _inputDirection);
        _characterController.Move(_currentVelocity * Time.deltaTime);
        _currentVelocity = CalculateMoveGravity(_currentVelocity, _gravityConstant);
        _currentVelocity = CalculateMoveJump(_currentVelocity);
        _jumpCommand = false;
        CheckStateChange();
    }

    private Vector3 CalculateMoveInput(Vector3 currentVelocity, Vector2 inputDirection)
    {
        inputDirection.Normalize();
        currentVelocity.x = inputDirection.x * _movingSpeed;
        currentVelocity.z = inputDirection.y * _movingSpeed;
        return currentVelocity;
    }

    private Vector3 CalculateMoveGravity(Vector3 currentVelocity, float gravityConstant)
    {
        currentVelocity.y = gravityConstant;
        return currentVelocity;
    }

    private Vector3 CalculateMoveJump(Vector3 currentVelocity)
    {
        if (!_jumpCommand )
        {
            return currentVelocity;
        }

        currentVelocity.y = _jumpForce;
        _jumpThisFrame = true;
        return currentVelocity;
    }

    private void CheckStateChange()
    {
        if (_jumpThisFrame || !_characterController.isGrounded)
        {
            var nextState = GetJumpState();
            ChangeState(nextState);
        }
    }

    private void SetupJumpValues()
    {
        float timeToApex = _jumpDuration / 2f;
        _jumpForce = (2 * _jumpHeight) / timeToApex;
    }


    protected virtual IMovementState GetJumpState()
    {
        return _factory.Create<StateJump>();
    }

}
