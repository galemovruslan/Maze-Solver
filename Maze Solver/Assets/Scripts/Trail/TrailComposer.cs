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

    private void Redraw()
    {
        var points = _trail.GetTrail();
        Vector3[] trailArray = new Vector3[points.Count];

        for (int pointIndex = 0; pointIndex < points.Count; pointIndex++)
        {
            trailArray[pointIndex] = new Vector3(points[pointIndex].x, points[pointIndex].y + _height, points[pointIndex].z);
        }
        if (_renderer.positionCount != points.Count)
        {
            _renderer.positionCount = points.Count;
        }
        _renderer.SetPositions(trailArray);
    }

    private void Update()
    {
        if (_trail == null)
        {
            Initialize();
        }

        _trail.UpdateTrail();
        Redraw();
    }

    private void Initialize()
    {
        _brush = FindObjectOfType<PlayerSpawner>().Player.GetComponent<TrailBrush>();
        _trail = new DynamicTrail(_brush, _minDistance);
        _trail.TrailChange += Redraw;
    }

}
