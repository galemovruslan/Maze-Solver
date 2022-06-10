
using UnityEngine;
using UnityEngine.InputSystem;

public interface IMovementState
{
    string StateName { get; }

    void Init(Vector3 velocity);

    void Move(Vector2 inputDirection, bool jumpCommand);

    void ChangeState(IMovementState nextState);

}
