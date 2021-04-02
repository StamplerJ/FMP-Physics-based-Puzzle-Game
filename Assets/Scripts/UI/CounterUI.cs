using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CounterUI : MonoBehaviour
{
    [SerializeField] private Button minus, plus;
    [SerializeField] private Text amount;

    private int wallCounter;

    public void OnMinusClick()
    {
        if (wallCounter > 0)
        {
            wallCounter--;
        }
        
        UpdateAmount();
    }

    public void OnPlusClick()
    {
        wallCounter++;
        UpdateAmount();
    }

    private void UpdateAmount()
    {
        amount.text = wallCounter.ToString();
    }
}
