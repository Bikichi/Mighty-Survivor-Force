using System;
using UnityEngine;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour
{
    [SerializeField] public float maxHealth;
	[SerializeField] public float currentHealth;
    [SerializeField] public float defense = 0f;
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

	public virtual void TakeDamage(float damage, bool isCrit = false)
	{
        float finalDamage = Mathf.Max(damage - defense, 1);

        currentHealth = Mathf.Max(currentHealth - finalDamage, 0);
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
