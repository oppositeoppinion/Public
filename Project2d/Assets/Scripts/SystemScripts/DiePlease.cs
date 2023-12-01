using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiePlease : MonoBehaviour
{
    [Range(0.1f, 100f)]
    [SerializeField] private float _rotateRateInSeconds=0.5f;
    [Range(0, byte.MaxValue)]
    [SerializeField] private byte _deathFading=130;
    [SerializeField] private Ease _ease=Ease.Linear;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private EnemyAnimatorController _animationScript;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _animationScript = GetComponent<EnemyAnimatorController>();
    }
    private void OnDestroy()
    {
        DOTween.Clear(this);
    }
    public void Die()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        _renderer.color = new Color32(255, 255, 255, _deathFading);
        _animationScript.StopAnimation();
        SceneSounds.Instance.PlayShortSounds(SceneSounds.Instance._AnimalDieSound, 0f);
        Tween();
    }
    [ContextMenu("tween")]
    private void Tween()
    {
        transform.DORotate(new Vector3(0, 180, 0), _rotateRateInSeconds)
        .SetEase(_ease)
        .SetLoops(-1, LoopType.Yoyo)
        ;
    }
}
