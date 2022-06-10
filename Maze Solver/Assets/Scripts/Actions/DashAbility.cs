using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : IAbility
{
    public IMovementState AbilityState => _dashState;

    public float Cooldown => _coolDown;

    private IMovementState _dashState;
    private IMovementFSM _movementFSM;

    private readonly float _coolDown = 1f;

    public DashAbility(IMovementState state, IMovementFSM movementFSM)
    {
        _dashState = state;
        _movementFSM = movementFSM;
    }

    public void Use()
    {
        _movementFSM.ForceState(_dashState);
    }

}
