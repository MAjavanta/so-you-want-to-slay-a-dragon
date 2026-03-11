using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth;
    private float _currentHealth;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }
    public void TakeDamage(float baseDamage)
    {
        _currentHealth -= baseDamage;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
