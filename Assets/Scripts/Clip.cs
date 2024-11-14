using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cores;

public class Clip : MonoBehaviour
{
    [SerializeField] private CoreMerger _merger;
    [SerializeField] private List<Transform> _placesForSeat;
    [SerializeField] private List<Line> _lines;
    [SerializeField] private UserInputHandler _userInputHandler;
    [SerializeField] private float _speedToClip;
    [SerializeField] private float _speedIntoClip = 0.1f;
    [SerializeField] private List<Core> _cores;

    private List<Core> _reservedCores = new();
    private int _maxQuantity = 6;
    private int _firstLine = 0;
    private int _secondLine = 1;
    private int _thirdLine = 2;
    private int _fourthLine = 3;
    private int _firstPosition = 0;
    private int _secondPosition = 1;
    private int _fistIndex = 0;
    private int _lastIndex = 1;
    private int _minValue = 0;

    public bool IsFullQuantity => _reservedCores.Count >= _maxQuantity;

    private void OnEnable()
    {
        _merger.Merged += UpdateCores;
    }

    private void OnDisable()
    {
        _merger.Merged -= UpdateCores;
    }

    private void UpdateCores(Core coreForDelete, Core coreForDelete2, Core newCore)
    {
        Remove(coreForDelete);
        Remove(coreForDelete2);

        _reservedCores.Add(newCore);
        _cores.Add(newCore);
    }

    public void Add(Core core)
    {
        _reservedCores.Add(core);

        var last = core;

        if (_cores.Count <= _maxQuantity)
        {
            if (IsMatch(_firstLine))
                MoveToClip(last, _firstLine);
            else if (IsMatch(_secondLine))
                MoveToClip(last, _secondLine);
            else if (IsMatch(_thirdLine))
                MoveToClip(last, _thirdLine);
            else if (IsMatch(_fourthLine))
                MoveToClip(last, _fourthLine);
        }
    }

    public IEnumerator FindMatch(Core core)
    {
        if (_cores.Count <= _maxQuantity)
        {
            yield return new WaitForSeconds(_speedToClip);

            _cores.Add(core);
            _merger.Merge(_cores);
            Insert();
        }
    }

    public void RemoveFirstElement(Transform target)
    {
        if (_cores.Count > _minValue)
            _cores[_fistIndex].MoveToEnemy(target);
    }

    public void Remove()
    {
        if (_cores.Count > _minValue)
        {
            Core core = _cores[_fistIndex];

            Remove(core);
        }
    }

    private void MoveToClip(Core last, int lineIndex)
    {
        last.Mover.MoveToClip(last.transform, _lines[lineIndex].GetPoints[_firstPosition].position, _speedToClip,
            _lines[lineIndex].GetPoints[_secondPosition].position, _placesForSeat[_firstPosition].transform.position);
    }

    private void Remove(Core core)
    {
        _reservedCores.Remove(core);
        _cores.Remove(core);
        Destroy(core.gameObject);
    }

    private void Insert()
    {
        if (_cores.Count <= _maxQuantity)
        {
            List<Transform> places = new(_cores.Count);

            for (int i = 0; i < _cores.Count; i++)
                places.Add(_placesForSeat[i]);

            for (int i = 0; i < _cores.Count; i++)
            {
                if (IsMatch(_firstLine))
                    MoveIntoClip(places, i);
                else if (IsMatch(_secondLine))
                    MoveIntoClip(places, i);
                else if (IsMatch(_thirdLine))
                    MoveIntoClip(places, i);
                else if (IsMatch(_fourthLine))
                    MoveIntoClip(places, i);
            }
        }
    }

    private void MoveIntoClip(List<Transform> places, int i) => 
        _cores[i].Mover.MoveIntoClip(_cores[i].transform, places[places.Count - _lastIndex - i].transform.position, _speedIntoClip); 

    private bool IsMatch(int value) => _userInputHandler.LineIndex == value;
}