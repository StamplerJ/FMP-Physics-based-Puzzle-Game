using UnityEngine;
using UnityEngine.UI;

public class MenuVictory : Singleton<MenuVictory>
{
    [SerializeField] private GameObject rootUI;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private Text coinAmount;

    private readonly float[] rewardPercentages = {0.33f, 0.5f, 0.9f};
    
    public void ShowMenu()
    {
        rootUI.SetActive(true);

        int achieved = GetStarsAchieved();
        for (int i = 0; i < achieved; i++)
        {
            stars[i].SetActive(true);
        }

        coinAmount.text = LevelTracker.Instance.CoinCounter.ToString();
    }

    private int GetStarsAchieved()
    {
        int stars = 0;

        float collected = LevelTracker.Instance.GetCollectedPercentage();

        print(collected);
        
        foreach (float percentage in rewardPercentages)
        {
            if (collected >= percentage)
            {
                stars++;
            }
        }

        return stars;
    }
}
