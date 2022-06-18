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

    private void Update()
    {
        if (_trail == null)
        {
            return;
        }

        _trail.UpdateTrail();
        Redraw();
    }
    public void Initialize(TrailBrush brush)
    {
        _brush = brush;
        _trail = new DynamicTrail(_brush, _minDistance);
        _trail.TrailChange += Redraw;
    }

    public void Hide()
    {
        this.enabled = false;
        _renderer.enabled = false;
    }

    public void Show()
    {
        this.enabled = true;
        _renderer.enabled = true;
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

}
