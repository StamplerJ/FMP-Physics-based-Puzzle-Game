using UnityEngine;
using UnityEngine.UI;

public class MenuVictory : Singleton<MenuVictory>
{
    [SerializeField] private GameObject rootUI;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private Text coinAmount;

    private readonly float[] rewardPercentages = {0.33f, 0.5f, 0.9f};

    private bool isLevelPerfectOnce;
    
    public void ShowMenu()
    {
        rootUI.SetActive(true);

        int achieved = GetStarsAchieved();
        for (int i = 0; i < achieved; i++)
        {
            stars[i].SetActive(true);
        }

        if (!IsLevelPerfectOnce && achieved == 3)
        {
            IsLevelPerfectOnce = true;
        }

        coinAmount.text = LevelTracker.Instance.CoinCounter.ToString();
    }

    private int GetStarsAchieved()
    {
        int stars = 0;

        float collected = LevelTracker.Instance.GetCollectedPercentage();

        foreach (float percentage in rewardPercentages)
        {
            if (collected >= percentage)
            {
                stars++;
            }
        }

        return stars;
    }

    public bool IsLevelPerfectOnce
    {
        get => isLevelPerfectOnce;
        set => isLevelPerfectOnce = value;
    }
}
