using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject Player => _playerOnScene;

    [SerializeField] private GameObject _playerPrefab;

    private GameObject _playerOnScene;

    public void Spawn(Vector3 startPosition)
    {
        var player = Instantiate<GameObject>(_playerPrefab, startPosition, Quaternion.identity);
        _playerOnScene = player;
    }


}
