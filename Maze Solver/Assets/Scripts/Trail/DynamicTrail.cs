using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicTrail
{
    public event Action TrailChange;

    private float _minDistance;
    private List<Vector3> _points;
    private TrailBrush _brush;
    private Vector3 _lastPoint;
    public DynamicTrail(TrailBrush brush, float minDistance)
    {
        _brush = brush;
        _minDistance = minDistance;
        _points = new List<Vector3>();
        _lastPoint = brush.Position;
        _points.Add(_lastPoint);
    }

    public void UpdateTrail()
    {
        var newPoint = _brush.Position;
        if( (newPoint - _lastPoint).magnitude >= _minDistance)
        {
            _points.Add(newPoint);
            _lastPoint = newPoint;
            TrailChange?.Invoke();
        }
        _points[_points.Count - 1] = newPoint;
    }

    public List<Vector3> GetTrail()
    {
        return _points;
    }

}