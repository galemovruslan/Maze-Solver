using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject Player => _playerOnScene;

    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private GridComposer _gridComposer;
    [SerializeField] private CinemachineVirtualCamera _camera;

    private GameObject _playerOnScene;

    private void Start()
    {
        var player = Instantiate<GameObject>(_playerPrefab, _gridComposer.StartPosition, Quaternion.identity);
        _camera.Follow = player.transform;
        _camera.LookAt = player.transform;
        _playerOnScene = player;
    }
}
