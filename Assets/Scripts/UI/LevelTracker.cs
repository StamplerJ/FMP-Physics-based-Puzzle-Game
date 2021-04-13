using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelTracker : Singleton<LevelTracker>
{
    private int coinMax;
    private int coinCounter;

    private Text coinText;

    public override void Awake()
    {
        base.Awake();

        if (GameObject.Find(Names.InGameCanvas))
        {
            coinText = GameObject.Find(Names.CoinText)?.GetComponent<Text>();
            coinMax = GameObject.FindGameObjectsWithTag(Tags.Coin).Length;   
        }
    }

    private void Start()
    {
        UpdateCoinText();
    }

    public void OnCoinPickup()
    {
        coinCounter++;
        UpdateCoinText();
    }

    private void UpdateCoinText()
    {
        if (coinText != null)
        {
            coinText.text = $"Coins: {coinCounter}/{coinMax}";   
        }
    }

    public int CoinMax => coinMax;

    public int CoinCounter => coinCounter;
}
