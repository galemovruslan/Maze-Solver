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
    private Vector3 _currentPosition;
    public DynamicTrail(TrailBrush brush, float minDistance)
    {
        _brush = brush;
        _minDistance = minDistance;
        _points = new List<Vector3>();
        _lastPoint = brush.Position;
        _points.Add(_lastPoint);
    }

    public void TryAddToTrail()
    {
        if( (_currentPosition - _lastPoint).magnitude >= _minDistance)
        {
            _points.Add(_currentPosition);
            _lastPoint = _currentPosition;
            TrailChange?.Invoke();
        }
        //_points[_points.Count - 1] = _currentPosition;
    }

    public void UpdateTrail()
    {
        _currentPosition = _brush.Position;
        TryAddToTrail();
    }

    public List<Vector3> GetTrail()
    {
        var trail = new List<Vector3>(_points);
        trail.Add(_currentPosition);
        return trail;
    }

}