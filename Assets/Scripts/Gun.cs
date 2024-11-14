using UnityEngine;
using Cores;
using Enemies;

public class Gun : MonoBehaviour
{
    [SerializeField] private Core _core;
    [SerializeField] private Clip _clip;
    [SerializeField] private Enemy _enemy;

    private void OnDisable() => _enemy.Hited -= RemoveCore;

    public void Shoot() => _clip.RemoveFirstElement(_enemy.transform);

    public void Init(Enemy enemy)
    {
        _enemy = enemy;
        _enemy.Hited += RemoveCore;
    }

    private void RemoveCore() => _clip.Remove();
}