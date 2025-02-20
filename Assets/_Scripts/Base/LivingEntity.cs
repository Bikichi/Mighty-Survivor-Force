using System;
using UnityEngine;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour
{
    [SerializeField] protected float startingHealth;
	[SerializeField] public float currentHealth;
    [SerializeField] public bool IsActive { get; protected set; }
    [SerializeField] public bool IsDead { get; protected set; }

    public UnityEvent<float, float> onHealthChange;

	protected virtual void Start()
	{
        currentHealth = startingHealth;
		IsActive = true;
		IsDead = false;
	}

	public virtual void TakeDamage(float damage)
	{
        currentHealth -= damage;
        onHealthChange.Invoke(currentHealth, startingHealth);
        if (currentHealth <= 0 && !IsDead)
		{
			Die();
		}
	}

	protected virtual void Die()
	{
		IsActive = false;
		IsDead = true;
	}
}
