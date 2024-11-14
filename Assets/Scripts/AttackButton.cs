using UnityEngine;
using DG.Tweening;

public class AttackButton : MonoBehaviour
{
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private float _speed;

    public void PlayAnimation()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(_targetPosition, _speed));
        sequence.Append(transform.DOMove(_startPosition, _speed));
    }
}
