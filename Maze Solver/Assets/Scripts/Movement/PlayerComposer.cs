using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerComposer : MonoBehaviour
{
    [SerializeField] private CharacterController _controller;

    private IMovementFSM _movementFSM;

    private void Awake()
    {
        InputActions playerInput = new InputActions();
        playerInput.Enable();

        _movementFSM = new PlayerFSM(_controller);
        IMovementStateFactory stateFactory = new MovementFactoryImpl(playerInput, _movementFSM, _controller);

        _movementFSM.Init(stateFactory.Create<StateMoving>());
    }

    private void Update()
    {
        _movementFSM.Tick();
    }
}
