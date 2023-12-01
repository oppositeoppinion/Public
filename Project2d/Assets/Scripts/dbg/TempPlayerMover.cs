using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerMover : MonoBehaviour
{
    [SerializeField] private Transform _playerTRF;
    [SerializeField] private Transform _startpoint;
    [SerializeField] private Transform _endpoint;
    [SerializeField] private float _lerpingSpeed;
    [SerializeField] private float _lerpingMagnitude;
    
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _playerTRF.transform.position=Vector2.Lerp(_startpoint.position,_endpoint.position, _lerpingMagnitude/100f );
        _lerpingMagnitude += _lerpingSpeed;
        if (_lerpingMagnitude > 100f)
        {
            (_startpoint, _endpoint) = (_endpoint, _startpoint);
            _lerpingMagnitude = 0.0f;
        }
    }

}
