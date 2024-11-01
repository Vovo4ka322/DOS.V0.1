using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthViewer : MonoBehaviour
{
    [SerializeField] private EnemyHealth _healthBar;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private void OnEnable()
    {
        _healthBar.Changed += OnChanged;
    }

    private void OnDisable()
    {
        _healthBar.Changed -= OnChanged;
    }

    private void OnChanged(int value)
    {
        _textMeshProUGUI.text = value.ToString() + " / " + _healthBar.MaxHealth;
    }
}
