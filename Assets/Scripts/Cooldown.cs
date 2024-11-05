using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour
{
    [SerializeField] private float _time;

    public bool CanUse {  get; private set; }

    private void Awake()
    {
        CanUse = true;
    }

    public void LaunchTimer() => StartCoroutine(StartTimer());

    private IEnumerator StartTimer()
    {
        CanUse = false;

        yield return new WaitForSeconds(_time);

        CanUse = true;
    }
}
