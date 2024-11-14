using System;
using System.Collections.Generic;
using UnityEngine;
using Scores;

namespace Cores
{
    public class CoreMerger : MonoBehaviour
    {
        [SerializeField] private Clip _clip;
        [SerializeField] private CoreSpawner _spawner;
        [SerializeField] private Transform _spawnPosition;
        [SerializeField] private Score _score;

        private int _greenCoreValue = 1;
        private int _blueCoreValue = 2;
        private int _redCoreValue = 3;
        private int _yellowCoreValue = 4;
        private int _scoreForMergeGreenCores = 2;
        private int _scoreForMergeBlueCores = 4;
        private int _scoreForMergeRedCores = 6;
        private int _minValue = 2;
        private int _lastValue = 1;
        private int _penultimateValue = 2;
        private int _lastIndex = 1;

        public event Action<Core, Core, Core> Merged;

        public void Merge(List<Core> cores)
        {
            while (CanMerge(cores))
            {
                int color = -1;
                int lastIndex = cores.Count - _lastIndex;

                if (IsMatch(cores[lastIndex], _greenCoreValue))
                    color = AssignValueForColor(_blueCoreValue, _scoreForMergeGreenCores);
                else if (IsMatch(cores[lastIndex], _blueCoreValue))
                    color = AssignValueForColor(_redCoreValue, _scoreForMergeBlueCores);
                else if (IsMatch(cores[lastIndex], _redCoreValue))
                    color = AssignValueForColor(_yellowCoreValue, _scoreForMergeRedCores);

                Merged?.Invoke(cores[lastIndex], cores[lastIndex - _lastValue], _spawner.Spawn(color, cores[lastIndex].transform.position));
            }

            for (int i = 0; i < cores.Count; i++)
            {
                cores[i].RemoveRigidbody();
            }
        }

        private int AssignValueForColor(int value, int valueForScore)
        {
            int color = value;
            _score.Change(valueForScore);

            return color;
        }

        private bool IsMatch(Core core, int value) => core.Value == value;

        private bool CanMerge(List<Core> cores)
        {
            if (cores.Count < _minValue)
                return false;

            return cores[^_lastValue].Value == cores[^_penultimateValue].Value && cores[^_lastValue].Value != _yellowCoreValue;
        }
    }
}