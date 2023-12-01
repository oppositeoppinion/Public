using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    private const float _percentage=100f;
    private const float _deltaTimeCompensator=200;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private float _interpolledCameraSpeed = 2f;
    [SerializeField] private float _notTrackingDistance = 1f;
    [SerializeField] private float _stopFollowDist=0.2f;
    [SerializeField] private float _CameraOffsetY=2f;
    [SerializeField] private bool _followingStarted;


   
    void Start()
    {
        if (_playerTransform == null) Debug.LogWarning($"attach player transform through inspector {gameObject.name}");
    }
    
    



    private void FixedUpdate()
    {
        var offsetedCameraPosition = new Vector2(_playerTransform.position.x, _playerTransform.position.y - _CameraOffsetY);
        var offSetedDistance = ((Vector2)transform.position - offsetedCameraPosition).magnitude; //get distance betveen offseted camera and player
        if (!_followingStarted && offSetedDistance > _notTrackingDistance)  
        {
            _followingStarted = true;
        }
        if (_followingStarted&& offSetedDistance > _stopFollowDist)
        {
            //offseting camera
            var offsetedPlayerPos = new Vector2(_playerTransform.position.x, _playerTransform.position.y-_CameraOffsetY);
            var cameraPos2d = Vector2.Lerp(transform.position, offsetedPlayerPos, _interpolledCameraSpeed / _percentage*Time.deltaTime*_deltaTimeCompensator);
            transform.position = new Vector3(cameraPos2d.x, cameraPos2d.y, transform.position.z); //to prevent changing of camera transform.position.z
        }
        if (_followingStarted && offSetedDistance <= _stopFollowDist) _followingStarted = false;
    }
}



