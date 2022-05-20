using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator), typeof(CharacterController))]
public class PlayerComposer : MonoBehaviour
{
    private CharacterController _controller;
    private IMovementFSM _movementFSM;
    private Animator _animator;

    private void Awake()
    {
        InputActions playerInput = new InputActions();
        playerInput.Enable();

        _controller = GetComponent<CharacterController>();
        _movementFSM = new PlayerFSM(_controller);
        IMovementStateFactory stateFactory = new MovementFactoryImpl(playerInput, _movementFSM, _controller);
        _movementFSM.Init(stateFactory.Create<StateMoving>());

        _animator = GetComponent<Animator>();

    }

    private void Update()
    {
        _movementFSM.Tick();
    }
}
