using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Core _core;
    [SerializeField] private Clip _clip;

    [SerializeField] private Enemy _enemy;

    public Core Core => _core;

    private void OnDisable()
    {
        _enemy.Hit -= RemoveCore;
    }

    public void Shoot()
    {
        _clip.RemoveFirstElement(_enemy.transform.position);
    }

    public void Init(Enemy enemy)
    {
        _enemy = enemy;
        _enemy.Hit += RemoveCore;
    }

    private void RemoveCore()
    {
        _clip.Remove();
    }
}
