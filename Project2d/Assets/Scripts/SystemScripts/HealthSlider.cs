using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _slider = GetComponent<Slider>();   
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        _slider.value = currentHealth/maxHealth;
    }
}
