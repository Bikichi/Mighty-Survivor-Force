using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    private float _health;

    [SerializeField]
    private GameObject _experience;
    [SerializeField]
    private GameObject _skull;
    [SerializeField]
    private GameObject _enemy;

    public static float _damgeTaken;

    public bool NoExperience;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        _damgeTaken = damage;
        if(_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Play.quantityEnenyDie++;
        if (NoExperience == false)
        {
            Instantiate(_experience, gameObject.transform.position + new Vector3(0, 0.37f, 0), _experience.transform.rotation);
        }
        Instantiate(_skull, gameObject.transform.position + new Vector3(0, 1.5f, -0.5f), _skull.transform.rotation);
        Destroy(_enemy);
    }
}
