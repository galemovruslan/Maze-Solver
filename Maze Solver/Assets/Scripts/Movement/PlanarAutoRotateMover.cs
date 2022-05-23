using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanarAutoRotateMover : ICharacterMover
{
    private CharacterController _characterController;
    private float _rotationSpeed = .1f;
    private Vector3 _setVelocity = Vector3.forward;

    public PlanarAutoRotateMover(CharacterController characterController)
    {
        _characterController = characterController;
    }

    public bool isGrounded => _characterController.isGrounded;

    public Vector3 Velocity => _characterController.velocity;

    public void Move(Vector3 velocity)
    {
        _characterController.Move(velocity);
        Rotate(velocity, _characterController.transform);
    }

    private void Rotate(Vector3 velocity, Transform transform)
    {
        if (!CheckZeroVelocity(velocity))
        {
            _setVelocity = new Vector3(velocity.x, 0, velocity.z);
        }
        var nextRotation = Quaternion.LookRotation(_setVelocity, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, nextRotation, Time.deltaTime / _rotationSpeed);

    }

    private bool CheckZeroVelocity(Vector3 velocity)
    {
        return velocity.x == 0 && velocity.z == 0;
    }

}
