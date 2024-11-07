using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        _reservedCores.Remove(coreForDelete);
        _cores.Remove(coreForDelete);
        Destroy(coreForDelete.gameObject);

        _reservedCores.Remove(coreForDelete2);
        _cores.Remove(coreForDelete2);
        Destroy(coreForDelete2.gameObject);

        _reservedCores.Add(newCore);
        _cores.Add(newCore);
    }

    public void Add(Core core)
    {
        _reservedCores.Add(core);

        var last = core;

        if (_cores.Count <= _maxQuantity)
        {
            if (_userInputHandler.LineIndex == _firstLine)
                last.Mover.MoveToClip(last.transform, _lines[_firstLine].GetPoints[0].position, _speedToClip, _lines[_firstLine].GetPoints[1].position, _placesForSeat[0].transform.position);
            else if (_userInputHandler.LineIndex == _secondLine)
                last.Mover.MoveToClip(last.transform, _lines[_secondLine].GetPoints[0].position, _speedToClip, _lines[_secondLine].GetPoints[1].position, _placesForSeat[0].transform.position);
            else if (_userInputHandler.LineIndex == _thirdLine)
                last.Mover.MoveToClip(last.transform, _lines[_thirdLine].GetPoints[0].position, _speedToClip, _lines[_thirdLine].GetPoints[1].position, _placesForSeat[0].transform.position);
            else if (_userInputHandler.LineIndex == _fourthLine)
                last.Mover.MoveToClip(last.transform, _lines[_fourthLine].GetPoints[0].position, _speedToClip, _lines[_fourthLine].GetPoints[1].position, _placesForSeat[0].transform.position);
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
        if (_cores.Count > 0)
        {
            _cores[0].MoveToEnemy(target);
        }
    }

    public void Remove()
    {
        if (_cores.Count > 0)
        {
            Core core = _cores[0];

            _reservedCores.Remove(core);
            _cores.Remove(core);
            Destroy(core.gameObject);
        }
    }

    private void Insert()
    {
        if (_cores.Count <= _maxQuantity)
        {
            List<Transform> places = new(_cores.Count);

            for (int i = 0; i < _cores.Count; i++)
            {
                places.Add(_placesForSeat[i]);
            }

            for (int i = 0; i < _cores.Count; i++)
            {
                if (_userInputHandler.LineIndex == _firstLine)
                    _cores[i].Mover.MoveIntoClip(_cores[i].transform, places[places.Count - 1 - i].transform.position, _speedIntoClip);
                else if (_userInputHandler.LineIndex == _secondLine)
                    _cores[i].Mover.MoveIntoClip(_cores[i].transform, places[places.Count - 1 - i].transform.position, _speedIntoClip);
                else if (_userInputHandler.LineIndex == _thirdLine)
                    _cores[i].Mover.MoveIntoClip(_cores[i].transform, places[places.Count - 1 - i].transform.position, _speedIntoClip);
                else if (_userInputHandler.LineIndex == _fourthLine)
                    _cores[i].Mover.MoveIntoClip(_cores[i].transform, places[places.Count - 1 - i].transform.position, _speedIntoClip);
            }
        }
    }
}