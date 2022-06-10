using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility 
{
    float Cooldown { get; }
    IMovementState AbilityState { get; }
    void Use();
}
