using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cores
{
    public class Core : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private List<CoreDataAppropriator> _appropriators;
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        [SerializeField] private float _flightSpeed = 100f;

        private int _minValue = 1;
        private int _maxValue = 4;

        [field: SerializeField] public CoreMover Mover { get; private set; }

        [field: SerializeField] public Collider Collider { get; private set; }

        [field: SerializeField] public int Value { get; private set; }

        public int Damage => _damage;

        private void Awake()
        {
            Value = Random.Range(_minValue, _maxValue + 1);

            SetData(Value);
        }

        public void MoveToEnemy(Transform target)
        {
            Mover.MoveToEnemy(transform, target, _flightSpeed);
        }

        public void SetData(int value)
        {
            CoreDataAppropriator dataAppropriator = _appropriators.FirstOrDefault(i => i.Value == value);

            if (dataAppropriator != null)
            {
                Value = value;
                _renderer.material = dataAppropriator.Material;
                _damage = dataAppropriator.Damage;
            }
        }

        public void RemoveRigidbody()
        {
            Destroy(_rigidbody);
            Collider.isTrigger = true;
        }
    }
}