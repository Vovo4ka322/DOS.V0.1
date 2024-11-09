using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private TextMeshProUGUI[] _scoreText;
    [SerializeField] private TextMeshProUGUI[] _highScoreText;

    private void OnEnable()
    {
        _score.ValueChanged += OnValueChanged;
        _score.HighValueChanged += OnHighValueChanged;
    }

    private void Start() => SortOut(_score.HighValue, _highScoreText);

    private void OnDisable()
    {
        _score.ValueChanged -= OnValueChanged;
        _score.HighValueChanged -= OnHighValueChanged;
    }

    private void OnValueChanged(float value) => SortOut(value, _scoreText);

    private void OnHighValueChanged(float value) => SortOut(value, _highScoreText);

    private void Change(float value, TextMeshProUGUI text) => text.text = value.ToString();

    private void SortOut(float value, TextMeshProUGUI[] texts)
    {
        foreach (var score in texts)
            Change(value, score);
    }
}