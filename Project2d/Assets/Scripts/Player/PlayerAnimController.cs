using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _renderer;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        #region EventsForAudio

        if (Input.GetKeyDown(KeyCode.D))//&&!_jumpAnimation
        {
            _animator.SetBool("isWalking", true);
            if (_renderer.flipX == true) _renderer.flipX = false;
        }
        if (Input.GetKeyDown(KeyCode.A))//&& !_jumpAnimation
        {
            _animator.SetBool("isWalking", true);
            if (_renderer.flipX != true) _renderer.flipX = true;
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (!Input.GetKey(KeyCode.D)) HorizontalInputStopped();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (!Input.GetKey(KeyCode.A)) HorizontalInputStopped();
        }
        #endregion
        
    }
    public void IsplayerJumping(bool jumping)
    {
        if (jumping) _animator.SetBool("isJumping", true);
        if (!jumping) _animator.SetBool("isJumping", false);
    }
    public void DashingStart()
    {
        _animator.speed = 4f;
    }
    public void DashingEnd()
    {
        _animator.speed = 1f;
    }
    public void Death()
    {
        _animator.SetTrigger("isDeath");
    }
    public void AttackAniamtion()
    {
        _animator.SetTrigger("sword_attack");
    }

    [ContextMenu("dbgSwitch")]
    private void DBG()
    {
        _animator.SetBool("isWalking", true);
    }
    private void RunAnimSwitch(Vector2 direction)
    {
        

        if (direction == Vector2.up)
        {
            _animator.SetBool("isJumping", true);
        }
        if (direction == Vector2.down)
        {
            _animator.SetBool("isJumping", false);
        }

        Debug.Log(direction);
    }
    private void HorizontalInputStopped()
    {
        _animator.SetBool("isWalking", false);
    }
    
}
