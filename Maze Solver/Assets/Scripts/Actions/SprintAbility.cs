using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintAbility : IAbility
{
    public float Cooldown => _cooldown;

    public IMovementState AbilityState => _sprintState;

    private StateSprint _sprintState;
    private IMovementFSM _movementFSM;
    private readonly float _cooldown;

    public SprintAbility(IMovementState state, IMovementFSM movementFSM)
    {
        _sprintState = state as StateSprint;
        _movementFSM = movementFSM;
        _cooldown = _sprintState.Duration + 3f;
    }

    public void Use()
    {
        _movementFSM.ForceState(_sprintState);
    }

    public bool CheckPrerequsites()
    {
        return _movementFSM.CurrentState.StateName == MovementNames.MoveName;
    }
}
