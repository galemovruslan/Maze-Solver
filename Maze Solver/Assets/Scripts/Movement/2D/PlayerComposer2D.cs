using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComposer2D : PlayerComposer
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

        _stateFactory = new MovementFactory2D(factoryParameters);
        _movementFSM.Init(_stateFactory.Create<StateMove2D>());
        
        _animator = GetComponent<Animator>();
        _animationPlayer = new AnimationPlayer(_animator, _characterMover);
    }


}
