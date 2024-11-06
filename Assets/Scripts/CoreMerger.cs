using System;
using System.Collections.Generic;
using UnityEngine;

public class CoreMerger : MonoBehaviour
{
    [SerializeField] private Clip _clip;
    [SerializeField] private CoreSpawner _spawner;
    [SerializeField] private Transform _spawnPosition;

    private int _greenCoreValue = 1;
    private int _blueCoreValue = 2;
    private int _redCoreValue = 3;
    private int _yellowCoreValue = 4;

    public event Action<Core, Core, Core> Merged;

    public void Merge(List<Core> cores)
    {
        while (CanMerge(cores))
        {
            int color = -1;
            int lastIndex = cores.Count - 1;

            if (cores[lastIndex].Value == _greenCoreValue)
            {
                color = _blueCoreValue;
            }
            else if (cores[lastIndex].Value == _blueCoreValue)
            {
                color = _redCoreValue;
            }
            else if (cores[lastIndex].Value == _redCoreValue)
            {
                color = _yellowCoreValue;
            }

            //cores.Add(_spawner.Spawn(color, cores[lastIndex].transform.position));

            Merged?.Invoke(cores[lastIndex], cores[lastIndex - 1], _spawner.Spawn(color, cores[lastIndex].transform.position));

            //DeleteCore(cores, cores[lastIndex]);    //particleSystem в позиции cores[lastIndex]   
            //DeleteCore(cores, cores[lastIndex - 1]);

        }

        for (int i = 0; i < cores.Count; i++)
        {
            cores[i].RemoveRigidbody();
        }
    }

    public void DeleteCore(List<Core> cores, Core core)
    {
        cores.Remove(core);
        Destroy(core.gameObject);
    }

    private bool CanMerge(List<Core> cores)
    {
        if (cores.Count < 2)
            return false;

        return cores[^1].Value == cores[^2].Value && cores[^1].Value != _yellowCoreValue;
    }
}
