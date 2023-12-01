using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private HealthSlider _healthSlider;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private ImpactOnDamage _impactScript;
    [SerializeField] private float _playerHealth=10f;
    [SerializeField] private float _playerMaxHealth=50f;

    private void Start()
    {
        _impactScript = GetComponent<ImpactOnDamage>();
        _healthSlider.UpdateHealthBar(_playerHealth, _playerMaxHealth);

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F9))
        {
            _playerHealth += 50;
            Debug.Log($"***CHEATCODE: +50 HP!***");
            _healthSlider.UpdateHealthBar(_playerHealth, _playerMaxHealth);
        }
    }
    public void TakeDamage(float damage)
    {
        _playerHealth -= damage;
        if (_playerHealth <= 0f) _playerController.Die();
        _impactScript.TakeImpulse();
        Debug.Log($"[{_playerHealth}][ health left]");
        _healthSlider.UpdateHealthBar(_playerHealth, _playerMaxHealth);
    }
}
