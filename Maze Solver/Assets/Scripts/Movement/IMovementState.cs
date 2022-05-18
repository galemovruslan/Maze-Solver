
using UnityEngine;
using UnityEngine.InputSystem;

public interface IMovementState 
{
    void Init(Vector3 velocity);

    void Move();


    void HandleMovement(InputAction.CallbackContext context);

    void HandleJump(InputAction.CallbackContext context);
}
