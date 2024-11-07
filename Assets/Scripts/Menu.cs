using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button _pause;
    [SerializeField] private Button _resume;
    [SerializeField] private Image _losePanel;
    [SerializeField] private Score _score;
    [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

    private void Awake()
    {
        Time.timeScale = 0f;
    }

    public void PressPause() => Time.timeScale = 0f;

    public void PressResume() => Time.timeScale = 1f;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void Lose()
    {
        Time.timeScale = 0f;
        _losePanel.gameObject.SetActive(true);
        _textMeshProUGUI.text = _score.Value.ToString();
    }

    public void Play()
    {
        Time.timeScale = 1f;
    }
}
