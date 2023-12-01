using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private ImpactOnDamage _impactScript;
    [SerializeField] private DiePlease _diePlease;
    [SerializeField] private float _damage;
    [SerializeField] private float _health;
    


    private void Awake()
    {
        _impactScript=GetComponent<ImpactOnDamage>();
        _diePlease=GetComponent<DiePlease>();   
    }
    public void TakeDdamage(float damage)
    {
        _health -= damage;
        _impactScript.TakeImpulse();
        //Debug.Log($"[{gameObject.name}][ is damaged by ][{damage}]");
        if (_health <= 0) _diePlease.Die();
    }
    public float GetDamage()
    {
        return _damage;
    }
    
}
