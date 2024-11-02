using DG.Tweening;
using UnityEngine;

public class CoreMover : MonoBehaviour
{
    public void Move(Transform transform, Vector3 target, float duration)
    {
        transform.DOMove(target, duration);
    }
}
