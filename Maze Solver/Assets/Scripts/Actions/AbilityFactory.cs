using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFactory
{
    private Dictionary<Type, IAbility> _map = new Dictionary<Type, IAbility>();
    private IMovementStateFactory _stateFactory;
    private IMovementFSM _movementFSM;

    public AbilityFactory(IMovementFSM movementFSM, IMovementStateFactory stateFactory)
    {
        _movementFSM = movementFSM;
        _stateFactory = stateFactory;

        PopulateMap(movementFSM, stateFactory);
    }

    private void PopulateMap(IMovementFSM movementFSM, IMovementStateFactory stateFactory)
    {
        var dash = new DashAbility(stateFactory.Create<StateDash>(), movementFSM);
        var sprint = new SprintAbility(stateFactory.Create<StateSprint>(), movementFSM);

        _map.Add(typeof(DashAbility), dash);
        _map.Add(typeof(SprintAbility), sprint);
    }

    public IAbility Create<T>() where T : IAbility
    {
        return _map[typeof(T)];
    }

}
