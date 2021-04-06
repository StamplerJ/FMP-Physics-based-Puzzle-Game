using UnityEngine;
using UnityEngine.UI;

public class CounterUI : MonoBehaviour, ICounterListener
{
    [SerializeField] private Button minus, plus;
    [SerializeField] private Text amount;

    [SerializeField] private int min, max;

    private ICounterListener listener;

    private int counter = 0;

    public void OnMinusClick()
    {
        if (counter > min)
        {
            listener.OnMinusClick();
            counter--;
            UpdateAmount();
        }
    }

    public void OnPlusClick()
    {
        if (counter < max)
        {
            listener.OnPlusClick();
            counter++;
            UpdateAmount();
        }
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

    public int Counter
    {
        get => counter;
        set => counter = value;
    }
}