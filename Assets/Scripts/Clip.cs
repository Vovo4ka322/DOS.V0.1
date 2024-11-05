using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Clip : MonoBehaviour
{
    [SerializeField] private CoreMerger _merger;
    [SerializeField] private List<Core> _cores;
    [SerializeField] private List<Transform> _placesForSeat;
    [SerializeField] private List<Line> _lines;
    [SerializeField] private UserInputHandler _userInputHandler;
    [SerializeField] private float _speedToClip;

    private int _maxQuantity = 6;

    public bool IsFullQuantity => _cores.Count == _maxQuantity;

    public Core Core()
    {
        if (_cores.Count > 0)
            return _cores[0];

        return null;
    }

    public void Add(Core core)
    {
        var last = core;

        if (_cores.Count <= _maxQuantity)
        {
            if (_userInputHandler.LineIndex == 0)
                last.Mover.MoveToClip(last.transform, _lines[0].GetPoints[0].position, _speedToClip, _lines[0].GetPoints[1].position, _placesForSeat[0].transform.position);
            else if (_userInputHandler.LineIndex == 1)
                last.Mover.MoveToClip(last.transform, _lines[1].GetPoints[0].position, _speedToClip, _lines[1].GetPoints[1].position, _placesForSeat[0].transform.position);
            else if (_userInputHandler.LineIndex == 2)
                last.Mover.MoveToClip(last.transform, _lines[2].GetPoints[0].position, _speedToClip, _lines[2].GetPoints[1].position, _placesForSeat[0].transform.position);
            else if (_userInputHandler.LineIndex == 3)
                last.Mover.MoveToClip(last.transform, _lines[3].GetPoints[0].position, _speedToClip, _lines[3].GetPoints[1].position, _placesForSeat[0].transform.position);
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

    public void RemoveFirstElement(Vector3 target)
    {
        if (_cores.Count > 0)
            _cores[0].MoveToEnemy(target);
    }

    public void Remove()
    {
        if (_cores.Count > 0)
            _merger.DeleteCore(_cores, _cores[0]);
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
                if (_userInputHandler.LineIndex == 0)
                    _cores[i].Mover.MoveIntoClip(_cores[i].transform, places[places.Count - 1 - i].transform.position, 0.1f);
                else if (_userInputHandler.LineIndex == 1)
                    _cores[i].Mover.MoveIntoClip(_cores[i].transform, places[places.Count - 1 - i].transform.position, 0.1f);
                else if (_userInputHandler.LineIndex == 2)
                    _cores[i].Mover.MoveIntoClip(_cores[i].transform, places[places.Count - 1 - i].transform.position, 0.1f);
                else if (_userInputHandler.LineIndex == 3)
                    _cores[i].Mover.MoveIntoClip(_cores[i].transform, places[places.Count - 1 - i].transform.position, 0.1f);
            }
        }
    }
}