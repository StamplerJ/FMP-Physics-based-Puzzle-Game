using UnityEngine;

public class EmptyLevel : MonoBehaviour
{
    private string levelName;

    private void Start()
    {
        levelName = PlayerPrefs.GetString(Names.SelectedLevel);
        
        EnterPlayMode.Instance.IsPlayMode = false;
        SaveSystem.Instance.Load(levelName);
        LevelTracker.Instance.SetupLevel();
    }
}
