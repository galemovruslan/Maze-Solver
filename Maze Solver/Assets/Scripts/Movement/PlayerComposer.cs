using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(CharacterController))]
public abstract class PlayerComposer : MonoBehaviour
{
    [SerializeField] protected MoveParameters _moveParameters;

    
    protected IMovementFSM _movementFSM;
    protected ICharacterMover _characterMover;
    protected IMovementStateFactory _stateFactory;
    protected CharacterController _controller;
    protected Animator _animator;
    protected AnimationPlayer _animationPlayer;

    protected virtual void Awake()
    {
        Bind();
    }

    protected abstract void Bind();

    private void OnEnable()
    {
        _movementFSM.OnStateChange += _animationPlayer.MakeTransition;
    }

    private void OnDisable()
    {
        _movementFSM.OnStateChange -= _animationPlayer.MakeTransition;

    }

    protected virtual void Update()
    {
        _movementFSM.Tick();
        _animationPlayer.Update();
    }
}
