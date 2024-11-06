using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyMovement _enemyMovement;
    [SerializeField] private EnemyType _type;

    private Transform _target;

    public event Action Hit;
    public event Action<EnemyType> Died;

    [field:SerializeField] public EnemyHealth Health {  get; private set; }

    public EnemyType Type => _type;

    public Transform Position => transform;

    private void Update()
    {
        _enemyMovement.Move(_target);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.TryGetComponent(out Core core))
        {
            Health.Lose(core.Damage);           
            Hit?.Invoke();

            if (Health.IsDead)
            {
                Died?.Invoke(_type);
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
