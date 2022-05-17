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
    private float _movingSpeed;

    private readonly float _gravityConstant = -0.05f;

    public StateMoving(IMovementFSM stateMachine, IMovementStateFactory factory, CharacterController characterController, float movingSpeed)
    {
        _movementFSM = stateMachine;
        _factory = factory;
        _characterController = characterController;
        _movingSpeed = movingSpeed;
    }
    public void Init()
    {
        
    }

    public void HandleMovement(InputAction.CallbackContext context)
    {
        _inputDirection = context.ReadValue<Vector2>();
    }

    public void HandleJump(InputAction.CallbackContext context)
    {
        bool jumpCommand = context.ReadValueAsButton();
        
        if (!jumpCommand || !_characterController.isGrounded)
        {
            return;
        }

        var nextState = _factory.Create<StateJump>();
        nextState.Init();

        _movementFSM.ChangeState(nextState);
    }

    public void ChangeState(IMovementState newState)
    {
        
    }

    public void Move()
    {
        var intputMove = CalculateMoveFromInput();
        var gravityMove = CalculateMoveFromGravity();

        _characterController.Move(intputMove + gravityMove);
    }

    private Vector3 CalculateMoveFromInput()
    {
        return new Vector3(_inputDirection.x, 0, _inputDirection.y).normalized 
            * _movingSpeed 
            * Time.deltaTime;
    }

    private Vector3 CalculateMoveFromGravity()
    {
        return new Vector3(0, _gravityConstant, 0) * Time.deltaTime;
    }

    
}
