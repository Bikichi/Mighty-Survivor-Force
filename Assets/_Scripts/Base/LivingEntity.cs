using System;
using UnityEngine;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour
{
    [SerializeField] public float maxHealth;
	[SerializeField] public float currentHealth;
    [SerializeField] public bool IsActive { get; protected set; }
    [SerializeField] public bool IsDead { get; protected set; }

    public UnityEvent<float, float> onHealthChange;

	public UnityEvent onDeath;

	protected virtual void Awake()
	{
        currentHealth = maxHealth;
		IsActive = true;
		IsDead = false;
	}

	public virtual void TakeDamage(float damage)
	{
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        onHealthChange?.Invoke(currentHealth, maxHealth);
        if (currentHealth <= 0 && !IsDead)
		{
			Die();
		}
	}

	protected virtual void Die()
	{
		IsActive = false;
		IsDead = true;
		onDeath?.Invoke();
	}
}
