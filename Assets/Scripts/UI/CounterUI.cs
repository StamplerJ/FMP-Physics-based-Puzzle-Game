using System.Collections.Generic;
using Grid;
using UnityEngine;
using UnityEngine.UI;

public class CounterUI : MonoBehaviour, ICounterListener
{
    [SerializeField] private Button minus, plus;
    [SerializeField] private Text amount;
    
    private ICounterListener listener;
    
    private int counter = 0;

    public void OnMinusClick()
    {
        if (counter > 0)
        {
            listener.OnMinusClick();
            counter--;
        }
        
        UpdateAmount();
    }

    public void OnPlusClick()
    {
        listener.OnPlusClick();
        counter++;
        UpdateAmount();
    }

    private void UpdateAmount()
    {
        amount.text = counter.ToString();
    }
    
    public ICounterListener Listener
    {
        get => listener;
        set => listener = value;
    }
}
