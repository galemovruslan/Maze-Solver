using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputValue<T>
{
    event Action<T> ValueChange;
    public T Value { get; }
}
