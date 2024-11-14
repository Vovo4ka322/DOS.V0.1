using Enemies;
using System;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public event Action Touched;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Enemy enemy))
            Touched?.Invoke();
    }
}
