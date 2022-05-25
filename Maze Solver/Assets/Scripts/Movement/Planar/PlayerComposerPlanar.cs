using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComposerPlanar : PlayerComposer
{
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
    }

}
