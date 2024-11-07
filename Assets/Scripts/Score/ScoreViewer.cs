using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreViewer : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TextMeshProUGUI _highScoreText;
    [SerializeField] private TextMeshProUGUI _defeatText;

    private void OnEnable()
    {
        _score.ValueChanged += OnValueChanged;
        _score.HighValueChanged += OnHighValueChanged;
    }

    private void Start()
    {
        Change(_score.HighValue, _highScoreText);
    }

    private void OnDisable()
    {
        _score.ValueChanged -= OnValueChanged;
        _score.HighValueChanged -= OnHighValueChanged;
    }

    private void OnValueChanged(float value) => Change(value, _scoreText);

    private void OnHighValueChanged(float value) => Change(value, _highScoreText);

    private void Change(float value, TextMeshProUGUI text) => text.text = value.ToString();
}
