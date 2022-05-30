using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private GridComposer _gridComposer;
    [SerializeField] private CinemachineVirtualCamera _camera;
    private void Start()
    {
        var player = Instantiate<GameObject>(_player, _gridComposer.StartPosition, Quaternion.identity);
        _camera.Follow = player.transform;
        _camera.LookAt = player.transform;
    }
}
