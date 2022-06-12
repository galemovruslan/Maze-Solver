using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComposerPlanar : PlayerComposer
{
    [SerializeField] private Vector3Reference _lookDirection;

    private Camera _camera;

    protected override void Awake()
    {
        _camera = Camera.main;
        base.Awake();
    }

    protected override void Update()
    {
        _lookDirection.Value = GetCameraForward();
        base.Update();
    }

    private Vector3 GetCameraForward()
    {
        var cameraForward = _camera.transform.forward;
        cameraForward.y = 0;
        return cameraForward.normalized;
    }

    protected override void Bind()
    {
        InputActions playerInput = new InputActions();
        playerInput.Enable();

        _controller = GetComponent<CharacterController>();
        _characterMover = new PlanarAutoRotateMover(_controller);
        _movementFSM = new PlayerFSM();
        
        FactoryParameters factoryParameters = new FactoryParameters
        {
            inputActions = playerInput,
            characterController = _characterMover,
            stateMachine = _movementFSM,
            moveParameters = _moveParameters
            
        };

        _stateFactory = new MovementFactoryImpl(factoryParameters);
        _movementFSM.Init(_stateFactory.Create<StateMoving>());

        _animator = GetComponent<Animator>();
        _animationPlayer = new AnimationPlayer(_animator, _characterMover);

        _abilityFactory = new AbilityFactory(_movementFSM, _stateFactory);
        _actionHandler = new AbilityHandler(_abilityFactory.Create<SprintAbility>());
        _inputHandler = new InputHandler(playerInput, _movementFSM, _actionHandler);
    }

}
