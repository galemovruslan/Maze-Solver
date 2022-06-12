
using System;
using UnityEngine;

public interface IMovementFSM
{
    IMovementState CurrentState { get; }
    event Action<string> OnStateChange;
    void Tick();
    void Init(IMovementState newState);
    void ChangeState(IMovementState newState);
    void ForceState(IMovementState newState);

    void HandleMovement(Vector2 inputDirection);
    void HandleJump(bool jumpCommand);
}
