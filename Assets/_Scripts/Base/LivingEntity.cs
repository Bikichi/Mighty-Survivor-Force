using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    [SerializeField] protected float startingHealth;
	[SerializeField] public float currentHealth;
    [SerializeField] public bool IsActive { get; protected set; }
    [SerializeField] public bool IsDead { get; protected set; }

	public event Action OnDeath;

	protected virtual void Start()
	{
        currentHealth = startingHealth;
		IsActive = true;
		IsDead = false;
	}

	public virtual void TakeDamage(float damage)
	{
        currentHealth -= damage;

		if (currentHealth <= 0 && !IsDead)
		{
			Die();
		}
	}

	protected virtual void Die()
	{
		IsActive = false;
		IsDead = true;
		if (OnDeath != null)
			OnDeath();
	}
}
