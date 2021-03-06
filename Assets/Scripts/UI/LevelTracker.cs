using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelTracker : Singleton<LevelTracker>
{
    private int coinMax;
    private int coinCounter;

    private Text coinText;

    private bool isLevelFinished;

    public override void Awake()
    {
        base.Awake();

        SetupLevel();
    }

    public void SetupLevel()
    {
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
            coinText.text = coinCounter.ToString();   
        }
    }

    public float GetCollectedPercentage()
    {
        if (coinMax == 0)
            return 1f;
        
        return (float) coinCounter / coinMax;
    }

    public int CoinMax => coinMax;

    public int CoinCounter => coinCounter;

    public bool IsLevelFinished
    {
        get => isLevelFinished;
        set => isLevelFinished = value;
    }
}
