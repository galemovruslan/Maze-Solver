using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMoving : IMovementState
{
    public string StateName => MovementNames.MoveName;

    private ICharacterMover _characterController;
    private IMovementFSM _movementFSM;
    private IMovementStateFactory _factory;
    private Vector2 _inputDirection;
    private Vector3 _currentVelocity;
    private float _movingSpeed;
    private float _runningSpeed;
    private float _sprintingSpeed;
    private float _jumpForce = 10f;
    private bool _jumpCommand = false;
    private float _jumpHeight;
    private float _jumpDuration;

    private readonly float _gravityConstant = -0.5f;

    public StateMoving(IMovementFSM stateMachine, 
        IMovementStateFactory factory, 
        ICharacterMover characterController, 
        float runningSpeed, 
        float sprintingSpeed,
        float jumpHeight,
        float jumpDuration)
    {
        _movementFSM = stateMachine;
        _factory = factory;
        _characterController = characterController;
        _runningSpeed = runningSpeed;
        _sprintingSpeed = sprintingSpeed;
        _movingSpeed = _runningSpeed;
        _jumpHeight = jumpHeight;
        _jumpDuration = jumpDuration;
        SetupJumpValues();
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

    public void HandleSprint(InputAction.CallbackContext context)
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
        _currentVelocity = CalculateMoveJump(_currentVelocity );
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
        if (!_jumpCommand || !_characterController.isGrounded)
        {
            return currentVelocity;
        }

        currentVelocity.y = _jumpForce;
        _currentVelocity = currentVelocity;

        var nextState = _factory.Create<StateJump>();
        ChangeState(nextState);

        return currentVelocity;
    }

    private void SetupJumpValues()
    {
        float timeToApex = _jumpDuration / 2f;
        _jumpForce = (2 * _jumpHeight) / timeToApex;
    }

}
