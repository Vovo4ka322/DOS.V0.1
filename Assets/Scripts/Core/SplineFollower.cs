using SplineMesh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplineFollower : MonoBehaviour
{
    [SerializeField] private Spline _spline;
    [SerializeField] private float _speed;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _splineRate = 0f;
    [SerializeField] private Clip _clip;

    private void Update()
    {
        SetEnemyPosition();

        _splineRate += _speed * Time.deltaTime;

        //if (_splineRate <= _spline.nodes.Count - 1)
        //    Place();
    }

    public void Init(Enemy enemy)
    {
        _enemy = enemy;
    }

    private void SetEnemyPosition()
    {
        //_spline.nodes[1].Position = _enemy.transform.localPosition;
        _spline.nodes[1].Position = _enemy.transform.parent.TransformPoint(_enemy.transform.localPosition);
    }

    //private void Update()
    //{
    //    _splineRate += _speed * Time.deltaTime;

    //    if (_splineRate <= _spline.nodes.Count - 1)
    //        Place();
    //}

    //private void Place()
    //{
    //    CurveSample sample = _spline.GetSample(_splineRate);
    //    _clip.Core().transform.position = sample.location;

    //    //transform.position = sample.location;
    //}
}
