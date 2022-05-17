using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovementStateFactory
{
    IMovementState Create<T>()  where T : IMovementState;
}
