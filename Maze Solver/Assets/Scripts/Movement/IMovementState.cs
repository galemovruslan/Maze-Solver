
using UnityEngine;
using UnityEngine.InputSystem;

public interface IMovementState
{
    string StateName { get; }

    void Init(Vector3 velocity);

    void Move();

    void HandleMovement(InputAction.CallbackContext context);

    void HandleJump(InputAction.CallbackContext context);

    void HandleSprint(InputAction.CallbackContext context);
}
