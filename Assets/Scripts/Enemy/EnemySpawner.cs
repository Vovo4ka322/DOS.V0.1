using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Gun _gun;
    [SerializeField] private EnemyFactory _enemyFactory;

    private void Awake()
    {
        SpawnDefault();
    }

    private void Spawn(EnemyType enemyType = EnemyType.Lvl1)
    {
        Enemy enemy = _enemyFactory.Get(enemyType, _spawnPoint.position);

        InitGun(enemy);
    }

    private void SpawnDefault()
    {
        Enemy enemy = _enemyFactory.GetDefault(_spawnPoint.position);

        InitGun(enemy);
    }

    private void InitGun(Enemy enemy)
    {
        _gun.Init(enemy);
        enemy.Init(_gun.transform);
        enemy.Died += Spawn;   
    }
}
