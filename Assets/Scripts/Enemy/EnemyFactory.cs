using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyFactory", menuName = "Factory")]
public class EnemyFactory : ScriptableObject
{
    [SerializeField] private List<Enemy> _enemyPrefabs;

    public Enemy Get(EnemyType enemyType, Vector3 spawnPoint)
    {
        Enemy enemy = TakeType(enemyType);

        Enemy enemyToSpawn = CreateEnemy(spawnPoint, enemy);

        return enemyToSpawn;
    }

    public Enemy GetDefault(Vector3 spawnPoint)
    {
        Enemy enemy = _enemyPrefabs.FirstOrDefault(enemy => enemy.Type == EnemyType.Lvl1);

        Enemy enemyToSpawn = CreateEnemy(spawnPoint, enemy);

        return enemyToSpawn;
    }

    private Enemy CreateEnemy(Vector3 spawnPoint, Enemy enemy)
    {
        return Instantiate(enemy, spawnPoint, Quaternion.identity);
    }

    private Enemy TakeType(EnemyType enemyType)
    {
        switch (enemyType)
        {
            case EnemyType.Lvl1:
                return _enemyPrefabs.FirstOrDefault(enemy => enemy.Type == EnemyType.Lvl2);

            case EnemyType.Lvl2:
                return _enemyPrefabs.FirstOrDefault(enemy => enemy.Type == EnemyType.Lvl3);

            case EnemyType.Lvl3:
                return _enemyPrefabs.FirstOrDefault(enemy => enemy.Type == EnemyType.Lvl4);

            case EnemyType.Lvl4:
                return _enemyPrefabs.FirstOrDefault(enemy => enemy.Type == EnemyType.Lvl5);

            case EnemyType.Lvl5:
                return _enemyPrefabs.FirstOrDefault(enemy => enemy.Type == EnemyType.Lvl6);

            default: return _enemyPrefabs.FirstOrDefault(enemy => enemy.Type == EnemyType.Lvl1);
        }
    }
}
