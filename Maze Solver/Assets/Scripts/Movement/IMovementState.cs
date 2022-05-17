
using UnityEngine;
using UnityEngine.InputSystem;

public interface IMovementState 
{
    void Init();

    void Move();


    void HandleMovement(InputAction.CallbackContext context);

    void HandleJump(InputAction.CallbackContext context);
}
