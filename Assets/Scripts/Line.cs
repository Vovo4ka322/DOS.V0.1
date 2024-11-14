using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private List<Transform> _placesForSeat;
    [SerializeField] private List<Transform> _pointsToClip;

    public IReadOnlyList<Transform> GetPlaces => _placesForSeat;

    public IReadOnlyList<Transform> GetPoints => _pointsToClip;
}
