using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class HealthViewer : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _healthBar;
        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;
        [SerializeField] private Image _healthImage;

        private float _maxHealth;

        private void Awake()
        {
            _maxHealth = _healthBar.MaxHealth;

            OnChanged(_healthBar.Health);
        }

        private void OnEnable() => _healthBar.Changed += OnChanged;

        private void OnDisable() => _healthBar.Changed -= OnChanged;

        private void OnChanged(int value)
        {
            _textMeshProUGUI.text = value.ToString() + " / " + _healthBar.MaxHealth;
            _healthImage.fillAmount = Mathf.InverseLerp(0, _maxHealth, value);
        }
    }
}