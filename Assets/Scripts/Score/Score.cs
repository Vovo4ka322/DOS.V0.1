using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public event Action<float> ValueChanged;
    public event Action<float> HighValueChanged;

    public float Value { get; private set; }

    public float HighValue { get; private set; }

    private void Awake()
    {
        HighValue = PlayerPrefs.GetFloat("HighValue", 0);
    }

    public void Change(float value)
    {
        Value += value;
        ValueChanged?.Invoke(Value);

        UpdateHighValue();
    }

    private void UpdateHighValue()
    {
        if(Value > HighValue)
        {
            HighValue = Value;
            HighValueChanged?.Invoke(HighValue);
            PlayerPrefs.SetFloat("HighValue", HighValue);
        }
    }
}
