using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private List<Transform> _placesForSeat;
    [SerializeField] private List<Transform> _pointsToClip;

    [field: SerializeField] public Collider Collider { get; private set; }

    public IReadOnlyList<Transform> GetPlaces => _placesForSeat;

    public IReadOnlyList<Transform> GetPoints => _pointsToClip;
}
