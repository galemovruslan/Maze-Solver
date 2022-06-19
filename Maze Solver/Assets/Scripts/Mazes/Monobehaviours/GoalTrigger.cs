using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    public event Action OnGoalAchived;

    private SphereCollider _collider;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
    }

    public void SetRadius(float newRadius)
    {
        _collider.radius = newRadius;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerComposer>(out var playerComposer))
        {
            OnGoalAchived?.Invoke();
        }
    }
}
