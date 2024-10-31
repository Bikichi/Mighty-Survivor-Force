using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private GameObject _targetPlayer;
    private const string runParaname = "Run";
    public Animator anim;
    public EnemyScriptableObject enemyData;

    void Update()
    {
        MoveEnemy();
    }

    public void MoveEnemy()
    {
        if (CheckDistance.Instance.CalculateDistanceFromPlayerToEnemy(_targetPlayer.transform, transform) <= 3.5f)
        {
            anim.SetBool(runParaname, false);
            return;
        } 
        else
        {
            anim.SetBool(runParaname, true);
            transform.LookAt(_targetPlayer.transform, Vector3.up);
            transform.Translate(Vector3.forward * Time.deltaTime * enemyData.enemyMoveSpeed);
        }
    }
}
