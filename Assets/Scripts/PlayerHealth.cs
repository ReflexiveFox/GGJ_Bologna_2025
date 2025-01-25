using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action<int, int> OnHealthChanged;
    public static event Action OnPlayerDied;

    [Header("Player Health Settings")]
    [SerializeField, Tooltip("Maximum health of the player.")]
    private int maxHealth = 100;

    [SerializeField, Tooltip("Current health of the player.")]
    private int _currentHealth;

    public int CurrentHealth 
    { 
        get => _currentHealth;
        set
        {
            if (_currentHealth != value)
            {
                OnHealthChanged?.Invoke(value, maxHealth);
            }
            _currentHealth = Mathf.Clamp(value, 0, maxHealth);
            if(_currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a bullet
        if (collision.gameObject.TryGetComponent(out Enemy bullet))
        {
            TakeDamage(bullet.damage);
        }
    }

    private void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + CurrentHealth);
    }

    private void Die()
    {
        Debug.Log("Player died.");
        // Implement player death logic here (e.g., respawn, game over screen, etc.)
        gameObject.SetActive(false);
        OnPlayerDied?.Invoke();
    }
}
