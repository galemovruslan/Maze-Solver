using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterMover 
{
    bool isGrounded { get; }
    Vector3 Velocity { get; }

    bool IsTouchingWall();

    void Move(Vector3 velocity);

    
}