using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface IHandleInput 
{

    public void HandleAbility(InputAction.CallbackContext context);

    public void HandleJump(InputAction.CallbackContext context);

    public void HandleMovement(InputAction.CallbackContext context);

    public void HandleSprint(InputAction.CallbackContext context);

   

}
