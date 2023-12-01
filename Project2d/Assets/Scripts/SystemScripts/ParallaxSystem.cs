using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ParallaxSystem : MonoBehaviour
{
    private const float percente=0.01f;

    [SerializeField] private float _offsetX;
    [SerializeField] private float _offsetY;
    [SerializeField] private float _xMovementSpeed;
    [Tooltip("y axis parallax speed, relative to camera, in percent")]
    [Range(-100f, 200f)]
    [SerializeField] private float _yParallax;  //how much it follows camera, 100=same movement, 0 = not follow
    [Tooltip("x axis parallax speed, relative to camera, in percent")]
    [Range(-100f, 200f)]
    [SerializeField] private float _xParallax;


    private Camera _camera;
    private float _posX;
    private float _spriteSizeX;
    private float cameraDeltaX;
    private float cameraPrevisiousXPosition;
    
    void Start()
    {
        _camera = Camera.main;
        cameraPrevisiousXPosition = _camera.transform.position.x;
        _posX = _camera.transform.position.x + _offsetX;
        _spriteSizeX = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        MakeTwoCopies();

    }

    void Update()
    {
            var distanceFromCamera = transform.position.x - _camera.transform.position.x;
            if (distanceFromCamera > _spriteSizeX)
            {
                _posX -= _spriteSizeX + _offsetX;
            }
            if (distanceFromCamera < -_spriteSizeX)
            {
                _posX += _spriteSizeX + _offsetX;
            }
            cameraDeltaX= _camera.transform.position.x-cameraPrevisiousXPosition;
            _posX += cameraDeltaX * _xParallax*percente;
            _posX += _xMovementSpeed * Time.deltaTime;
            var posY = _camera.transform.position.y * _yParallax * percente + _offsetY;
            transform.position = new Vector2(_posX, posY);
            cameraPrevisiousXPosition= _camera.transform.position.x;
    }

    void Update2()
    {
        var distanceFromCamera = transform.position.x - _camera.transform.position.x;
        CameraNearEdgesCheck(distanceFromCamera);
        MoveSprite();
    }

    private void CameraNearEdgesCheck(float distanceFromCamera)
    {
        if (distanceFromCamera > _spriteSizeX)
        {
            _posX -= _spriteSizeX + _offsetX;
        }
        if (distanceFromCamera < -_spriteSizeX)
        {
            _posX += _spriteSizeX + _offsetX;
        }
    }
    private void MoveSprite()
    {
        cameraDeltaX = _camera.transform.position.x - cameraPrevisiousXPosition;
        _posX += cameraDeltaX * _xParallax * percente;
        _posX += _xMovementSpeed * Time.deltaTime;
        var posY = _camera.transform.position.y * _yParallax * percente + _offsetY;
        transform.position = new Vector2(_posX, posY);
        cameraPrevisiousXPosition = _camera.transform.position.x;
    }

    private void MakeTwoCopies() //makes copies of sprite, and lokates them on both sides of the original sprite
    {
        var rightPosition1 = new Vector3(transform.position.x + _spriteSizeX, transform.position.y, transform.position.z);
        var rightPosition2 = new Vector3(transform.position.x + _spriteSizeX*2, transform.position.y, transform.position.z);
        var leftPosition1 = new Vector3(transform.position.x - _spriteSizeX, transform.position.y, transform.position.z);
        var leftPosition2 = new Vector3(transform.position.x - _spriteSizeX*2, transform.position.y, transform.position.z);
        Copy(new Vector3[] {rightPosition1, rightPosition2, leftPosition1, leftPosition2 });
    }
    private void Copy(Vector3[] position) 
    {
        int n = 1;
        ParallaxSystem unchangedObjectHolder = null;
        ParallaxSystem paralaxComponent = gameObject.GetComponent<ParallaxSystem>();
        foreach ( var positionItem in position )
        {
            ParallaxSystem copy;
            if (unchangedObjectHolder==null) copy=Instantiate(paralaxComponent, positionItem, Quaternion.identity, transform);
            else copy = Instantiate(unchangedObjectHolder, positionItem, Quaternion.identity, transform);
            copy.enabled = false;
            copy.name = $"{gameObject.name}_copy number {n++}";
            unchangedObjectHolder=copy;
        }
    }
    

}
