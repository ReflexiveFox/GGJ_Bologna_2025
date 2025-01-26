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

    [SerializeField] private SoundData hitSound;

    public int CurrentHealth 
    { 
        get => _currentHealth;
        private set
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
        if (collision.gameObject.TryGetComponent(out Bullet bullet))
        {
            TakeDamage(bullet.damage);           
        }
        else if(collision.gameObject.TryGetComponent(out PlayerDead playerDead))
        {
            TakeDamage(maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;
        SoundManager.Instance.CreateSound().WithSoundData(hitSound).WithRandomPitch().WithPosition(transform.position).Play();
    }

    private void Die()
    {
        Debug.Log("Player died.");
        // Implement player death logic here (e.g., respawn, game over screen, etc.)
        gameObject.SetActive(false);
        OnPlayerDied?.Invoke();
    }
}
