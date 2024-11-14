using System;
using UnityEngine;

namespace Enemies
{
    public class EnemyHealth : MonoBehaviour
    {
        public event Action<int> Changed;

        [field: SerializeField] public int MaxHealth { get; private set; }

        [field: SerializeField] public int Health { get; private set; }

        public bool IsDead => Health <= 0;

        public void Lose(int damage)
        {
            Health = Mathf.Clamp(Health - damage, 0, MaxHealth);
            Changed?.Invoke(Health);
        }
    }
}