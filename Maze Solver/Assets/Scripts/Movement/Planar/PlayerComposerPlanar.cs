using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator), typeof(CharacterController))]
public class PlayerComposerPlanar : MonoBehaviour
{
    [SerializeField] private MoveParameters _moveParameters;

    private IMovementFSM _movementFSM;
    private ICharacterMover _characterMover;
    IMovementStateFactory _stateFactory;
    private CharacterController _controller;
    private Animator _animator;
    AnimationPlayer _animationPlayer;

    private void Awake()
    {
        InputActions playerInput = new InputActions();
        playerInput.Enable();

        _controller = GetComponent<CharacterController>();
        _characterMover = new PlanarAutoRotateMover(_controller);

        _movementFSM = new PlayerFSM();
        _stateFactory = new MovementFactoryImpl(playerInput, _movementFSM, _characterMover, _moveParameters);
        _movementFSM.Init(_stateFactory.Create<StateMoving>());
        
        _animator = GetComponent<Animator>();
        _animationPlayer = new AnimationPlayer(_animator, _characterMover);
    }

    private void OnEnable()
    {
        _movementFSM.OnStateChange += _animationPlayer.MakeTransition;
    }

    private void OnDisable()
    {
        _movementFSM.OnStateChange -= _animationPlayer.MakeTransition;

    }

    private void Update()
    {
        _movementFSM.Tick();
        _animationPlayer.Update();
    }
}
