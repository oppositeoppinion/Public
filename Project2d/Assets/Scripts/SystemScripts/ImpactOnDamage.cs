using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactOnDamage : MonoBehaviour
{
    [SerializeField] private EnemyControlller _enemyControlller;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private float _StrikingPower;
    [SerializeField] private Color32 _struggleColor= new Color32(246, 180, 180, 255);
    [SerializeField] private Color32 _oldColor;
    [SerializeField] private float _UnvunerabilityTime;
    [SerializeField] private float _speedAfterImpactInPercents;
    [SerializeField] private float _afretImpactSpeedTime;
    [SerializeField] private bool _isGOPlayer;

    private int _playerLayer;
    private int _enemyLayer;
    private void Start()
    {
        if (gameObject.name == "Player") _isGOPlayer = true;
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerLayer = LayerMask.NameToLayer("player");
        _enemyLayer = LayerMask.NameToLayer("enemy");
        _renderer = GetComponent<SpriteRenderer>();
        if (!_isGOPlayer) _enemyControlller = GetComponent<EnemyControlller>();
        _oldColor = _renderer.color;

    }
    private void FixedUpdate()
    {
        
    }
    [ContextMenu("TakeImpulse")]
    
    private IEnumerator LayerCollisionsTimer()
    {
        _renderer.color = _struggleColor;
        Physics2D.IgnoreLayerCollision(_playerLayer, _enemyLayer);
        yield return new WaitForSeconds(_UnvunerabilityTime);
        Physics2D.IgnoreLayerCollision(_playerLayer, _enemyLayer,false);
        _renderer.color = _oldColor;
    }
    public void TakeImpulse()
    {
        var direction = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y + _StrikingPower);

        if (!_isGOPlayer) _enemyControlller.SlowingInPercent(_speedAfterImpactInPercents, _afretImpactSpeedTime);
        _rigidbody.velocity = direction;
        SceneSounds.Instance.PlayShortSounds(SceneSounds.Instance._struckSound, 0f);
        StartCoroutine(LayerCollisionsTimer());
    }
}
