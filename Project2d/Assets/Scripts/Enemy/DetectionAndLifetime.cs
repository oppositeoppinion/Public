using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionAndLifetime : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private EnemyControlller _enemycontroller;
    [SerializeField] private int _framesBeforeSearch = 20;
    [SerializeField] private float _aggroDistance;
    [SerializeField] private float _destroyDistance;
    [SerializeField] private float _turnBackDistance;
    [SerializeField] private bool _playerIsRight;
    [SerializeField] private bool _isPursuePlayer;
    [SerializeField] private bool _isStopTurning;
    private int _counter;
    private bool _playerFound;
    private float _distance;


    private void Start()
    {
        FindPlayerGO();
        _enemycontroller = GetComponent<EnemyControlller>();
        print(ToString());
        Debug.Log(ToString());
    }
    private void FixedUpdate()
    {
        _counter++;
        if (_counter % _framesBeforeSearch == 0f || !_playerFound)
        {
            if (!_playerFound) CheckDistanceToPlayer();
            if (_playerFound) CheckDestroyDistance();
            if (_counter > Int32.MaxValue - _framesBeforeSearch * 2) _counter = 0;
        }
        if (_isPursuePlayer && _playerFound)
        {
            CheckDistanceToTurn();
        }
    }
    private void FindPlayerGO()
    {
        _player = GameObject.Find("Player").GetComponent<Transform>();
    }
    private void CheckDistanceToPlayer()
    {
        _distance = transform.position.x - _player.position.x;
        if (Mathf.Abs(_distance) < _aggroDistance) Unfreeze();
    }

    private void CheckDistanceToTurn()
    {
        var turnDistance = transform.position.x - _player.position.x;
        bool tempPlayerIsRight;
        if (turnDistance > 0) tempPlayerIsRight = false;
        else tempPlayerIsRight = true;
        if (tempPlayerIsRight != _playerIsRight && Mathf.Abs(turnDistance) > _turnBackDistance) 
        {
            _enemycontroller.SwitchSpeed();
            _playerIsRight = !_playerIsRight;
        }


    }
    private void CheckDestroyDistance()
    {
        var dist = transform.position.x - _player.position.x;
        if (Mathf.Abs(dist) > _destroyDistance)
        {
            Destroy(this.gameObject);
            Debug.Log($"[{gameObject.name}][ is destroyed]");
        }
    }
    private void Unfreeze()
    {
        Debug.Log($"[{gameObject.name}][ is unfreezed] [{_distance}]");
        _playerFound = true;
        if (_distance > 0) _playerIsRight = false;
        else _playerIsRight = true;
        _enemycontroller.StartFollowPlayer(_playerIsRight);
    }
    
}
