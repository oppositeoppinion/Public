using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class dbgAnimatorTest : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorController controller;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [ContextMenu("AnimatorTest")]
    private void DbgAnitest()
    {
        animator.speed = 30;
    }

}
