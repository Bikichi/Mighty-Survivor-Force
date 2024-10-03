using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    [SerializeField] protected float startingHealth;
	[SerializeField] protected float currentHealth;

    [SerializeField] protected RaycastHit lastHit;
    [SerializeField] public bool IsActive { get; protected set; }
    [SerializeField] public bool IsDead { get; protected set; }

	public event Action OnDeath;

	protected virtual void Start()
	{
        currentHealth = startingHealth;
		IsActive = true;
		IsDead = false;
	}

	public virtual void TakeHit(float damage, RaycastHit hit)
	{
		lastHit = hit;
		TakeDamage (damage);
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
