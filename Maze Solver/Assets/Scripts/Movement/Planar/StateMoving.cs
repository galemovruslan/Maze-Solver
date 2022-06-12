using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateMoving : IMovementState
{
    public virtual string StateName => MovementNames.MoveName;

    private Vector3Reference _forward;
    private ICharacterMover _characterController;
    private IMovementFSM _movementFSM;
    private float _runningSpeed;
    private float _sprintingSpeed;
    private float _jumpForce = 10f;
    private float _jumpHeight;
    private float _jumpDuration;
    private bool _jumpThisFrame = false;

    protected Vector3 _currentVelocity;
    protected float _movingSpeed;
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
        _forward = parameters.CameraVector;

        _movingSpeed = _runningSpeed;
        SetupJumpValues();
    }
    public virtual void Init(Vector3 velocity)
    {
        _jumpThisFrame = false;
        _currentVelocity = velocity;
    }

    public virtual void HandleSprint(InputAction.CallbackContext context)
    {
        _movingSpeed = context.ReadValueAsButton() ? _sprintingSpeed : _runningSpeed;
    }

  

    public virtual void ChangeState(IMovementState nextState)
    {
        nextState.Init(_currentVelocity);
        _movementFSM.ChangeState(nextState);
    }

    public virtual void Move(Vector2 inputDirection, bool jumpCommand)
    {
        _jumpCommand = jumpCommand;
        _inputDirection = inputDirection;
        _inputDirection = AllignToCamera(_inputDirection);
        _currentVelocity = CalculateMoveInput(_currentVelocity, _inputDirection);
        _characterController.Move(_currentVelocity * Time.deltaTime);
        _currentVelocity = CalculateMoveGravity(_currentVelocity, _gravityConstant);
        _currentVelocity = CalculateMoveJump(_currentVelocity, _jumpCommand);
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

    private Vector3 CalculateMoveJump(Vector3 currentVelocity, bool jumpCommand)
    {
        if (!jumpCommand)
        {
            return currentVelocity;
        }

        currentVelocity.y = _jumpForce;
        _jumpThisFrame = true;
        return currentVelocity;
    }

    protected virtual void CheckStateChange()
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

    private Vector2 AllignToCamera(Vector2 worldSpaceInput)
    {
        Vector3 input = new Vector3(worldSpaceInput.x, 0, worldSpaceInput.y);
        var correctionRotation = Quaternion.FromToRotation(Vector3.forward, _forward.Value);
        input = correctionRotation * input;
        Vector2 cameraSpaceInput = new Vector2(input.x, input.z);
        return cameraSpaceInput;
    }

    
}
