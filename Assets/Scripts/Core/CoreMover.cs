using DG.Tweening;
using UnityEngine;

namespace Cores
{
    public class CoreMover : MonoBehaviour
    {
        public void MoveToClip(Transform core, Vector3 firstTarget, float duration, Vector3 secondTarget, Vector3 thirdTarget)
        {
            Sequence sequence = DOTween.Sequence();

            int divisor = 3;

            duration /= divisor;

            sequence.Append(core.DOMove(firstTarget, duration));
            sequence.Append(core.DOMove(secondTarget, duration));
            sequence.Append(core.DOMove(thirdTarget, duration));
        }

        public void MoveIntoClip(Transform core, Vector3 target, float duration)
        {
            core.DOMove(target, duration);
        }

        public void MoveToEnemy(Transform core, Transform target, float speed)
        {
            core.DOMove(target.position, speed).SetSpeedBased();
        }
    }
}