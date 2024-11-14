using Cores;
using System;
using UnityEngine;
using Scores;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyMovement _enemyMovement;
        [SerializeField] private EnemyType _type;

        private Score _score;
        private Transform _target;

        public event Action Hited;
        public event Action<EnemyType> Died;

        [field: SerializeField] public EnemyHealth Health { get; private set; }

        [field: SerializeField] public int ScoreReward { get; private set; }

        public EnemyType Type => _type;

        private void OnTriggerEnter(Collider collision)
        {
            if (collision.gameObject.TryGetComponent(out Core core))
            {
                Health.Lose(core.Damage);
                Hited?.Invoke();

                if (Health.IsDead)
                {
                    Died?.Invoke(_type);
                    Destroy(gameObject);

                    _score.Change(ScoreReward);
                }
            }
        }

        private void Update()
        {
            _enemyMovement.Move(_target);
        }

        public void Init(Transform target, Score score)
        {
            _target = target;
            _score = score;
        }
    }
}