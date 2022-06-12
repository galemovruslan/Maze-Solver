using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Move Parameters", menuName = "Parameters/Move")]
public class MoveParameters : ScriptableObject
{

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _sprintSpeed;
    [SerializeField] private float _sprintTime;
    [Space]
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float _jumpTime;
    [SerializeField] private Vector3Reference _cameraVector;

    public float MoveSpeed => _moveSpeed; 
    public float SprintSpeed => _sprintSpeed;
    public float JumpHeight => _jumpHeight; 
    public float JumpTime => _jumpTime;
    public Vector3Reference CameraVector => _cameraVector;

    public float SprintTime => _sprintTime; 
}
