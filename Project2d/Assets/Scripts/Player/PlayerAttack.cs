using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private Vector3 _attackPointLeft;
    [SerializeField] private CircleCollider2D _attackpointCollider;
    [SerializeField] private PlayerAnimController _animationController;
    private bool _isAttackDelayed;

    
    private void Start()
    {
        _attackPoint = transform.Find("AttackPoint");
        _attackpointCollider=_attackPoint.GetComponent<CircleCollider2D>();
        _attackpointCollider.enabled = false;
        _animationController=GetComponent<PlayerAnimController>();
        _attackPointLeft = new Vector3(_attackPoint.localPosition.x * -1, _attackPoint.localPosition.y, _attackPoint.localPosition.z);
    }
    private IEnumerator AttackDelay()
    {
        _isAttackDelayed = true;
        yield return new WaitForSeconds(PlayerWeapons.Sword.DelayAfterAttack);
        _isAttackDelayed = false;
    }
    private IEnumerator PreAttack(bool isFacingRight)
    {
        yield return new WaitForSeconds(PlayerWeapons.Sword.DelayBeforeAttack);
        SceneSounds.Instance.PlayShortSounds(SceneSounds.Instance._swingSound, 0f);
        Collider2D[] hitedTargets;
        if (isFacingRight) hitedTargets = Physics2D.OverlapCircleAll(_attackPoint.position, _attackpointCollider.radius);
        else
        {
            var leftAttackpoint = transform.position + _attackPointLeft;
            hitedTargets = Physics2D.OverlapCircleAll(leftAttackpoint, _attackpointCollider.radius);
            //Debug.Log($"leftattack");
        }


        foreach (var target in hitedTargets)
        {
            if (target.TryGetComponent(out RigidIdentifier typeScript))
            {
                if (typeScript.Type == RigidType.Enemiy)
                {
                    if (target.TryGetComponent(out EnemyStats enemyStats))
                    {
                        enemyStats.TakeDdamage(PlayerWeapons.Sword.Damage);
                    }
                }
            }
        }

    }
    public void Attack(bool isFacingRight)
    {
        if (!_isAttackDelayed)
        {
            _animationController.AttackAniamtion();
            StartCoroutine(PreAttack(isFacingRight));
            StartCoroutine(AttackDelay());
        }
        
    }
    
}
