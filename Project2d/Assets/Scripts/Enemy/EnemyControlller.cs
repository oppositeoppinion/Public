using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControlller : MonoBehaviour
{
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] private float _enenmyMoveSpeed;
    [SerializeField] private bool _followPlayer;
    private Rigidbody2D _rigidBody;
    private EnemyAnimatorController _animController;
    private void Awake()
    {
        _enemyStats = GetComponent<EnemyStats>();
        _animController = GetComponent<EnemyAnimatorController>();
    }
    private void Start()
    {
        var enemyLayer = LayerMask.NameToLayer("enemy");
        Physics2D.IgnoreLayerCollision(enemyLayer, enemyLayer);//move to global script
        _rigidBody = GetComponent<Rigidbody2D>();
        
    }
    private void FixedUpdate()
    {
        if (_followPlayer)
        {
            var direction = new Vector2(_enenmyMoveSpeed, _rigidBody.velocity.y);
            _rigidBody.velocity = direction; 
        }
    }

    //private void 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out RigidIdentifier identifier))
        {
            if(identifier.Type == RigidType.Player) { 
                var playerStats=collision.gameObject.GetComponent<PlayerStats>();
                playerStats.TakeDamage(_enemyStats.GetDamage());
            }
        }
    }
    private IEnumerator moveSpeedReturn(float speedholder, float time)
    {
        yield return new WaitForSeconds(time);
        _enenmyMoveSpeed = speedholder;
    }

    public void StartFollowPlayer(bool isPlayerRight)
    {
        _followPlayer = true;
        if (!isPlayerRight)
        {
            _enenmyMoveSpeed = -_enenmyMoveSpeed;
        }
        else _animController.SwitchSide();

        _animController.StartBehaviour();
    }
    public void SlowingInPercent(float percent, float time)
    {
        if (percent < 0 || percent > 100) return;
        var speedHolder = _enenmyMoveSpeed;
        _enenmyMoveSpeed *= percent / 100;
        StartCoroutine(moveSpeedReturn(speedHolder,time));
    }
    
    public void SwitchSpeed()
    {
        _enenmyMoveSpeed *= -1;
        _animController.SwitchSide();
    }
}
