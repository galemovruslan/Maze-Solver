using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMoving : IMovementState
{
    private CharacterController _characterController;
    private IMovementFSM _movementFSM;
    private IMovementStateFactory _factory;
    private Vector2 _inputDirection;
    private Vector3 _currentVelocity;
    private float _movingSpeed;
    private float _jumpForce = 10f;
    private bool _jumpCommand = false;

    private readonly float _gravityConstant = -0.5f;

    public StateMoving(IMovementFSM stateMachine, IMovementStateFactory factory, CharacterController characterController, float movingSpeed)
    {
        _movementFSM = stateMachine;
        _factory = factory;
        _characterController = characterController;
        _movingSpeed = movingSpeed;
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
    
}
