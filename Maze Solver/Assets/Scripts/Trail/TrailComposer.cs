using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class TrailComposer : MonoBehaviour
{
    [SerializeField] float _height;
    [SerializeField] float _minDistance;

    private LineRenderer _renderer;
    private TrailBrush _brush;
    private DynamicTrail _trail;

    private void Awake()
    {
        _renderer = GetComponent<LineRenderer>();
    }

    private void OnTrailChange()
    {
        var points = _trail.GetTrail();
        points.ForEach(p => p.y = _height);

        _renderer.positionCount = points.Count;
        _renderer.SetPositions(points.ToArray());
    }

    private void Update()
    {
        if (_trail == null)
        {
            Initialize();
        }

        _trail.UpdateTrail();
    }

    private void Initialize()
    {
        _brush = FindObjectOfType<PlayerSpawner>().Player.GetComponent<TrailBrush>();
        _trail = new DynamicTrail(_brush, _minDistance);
        _trail.TrailChange += OnTrailChange;
    }

}
