using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _renderer;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
    }
    public void StartBehaviour() {
        _animator.SetTrigger("walking");
    }
    public void SwitchSide() 
    { 
        _renderer.flipX = !_renderer.flipX;
    }
    public void StopAnimation()
    {
        _animator.StopPlayback();
    }
}
