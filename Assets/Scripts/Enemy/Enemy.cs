using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyType _type;

    private Transform _target;

    public event Action Hit;
    public event Action<EnemyType> Dead;

    public EnemyType Type => _type;

    [field:SerializeField] public EnemyHealth Health {  get; private set; }

    public Transform Position => transform;

    private void Update()
    {
        _enemyMovement.Move(_target);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Core core))
        {
            Health.Lose(core.Damage);           
            Hit?.Invoke();

            if (Health.IsDead)
            {
                Dead?.Invoke(_type);
                Destroy(gameObject);
            }
        }
    }

    public void Init(Transform target)
    {
        _target = target;
    }
}

public enum EnemyType
{
    Lvl1, Lvl2, Lvl3, Lvl4, Lvl5, Lvl6
}
