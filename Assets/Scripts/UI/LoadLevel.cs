using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private string level;
    [SerializeField] private bool isPremade;
    
    public void OnLoadLevel()
    {
        if (isPremade)
        {
            SceneManager.LoadScene(level);   
        }
        else
        {
            if (level.Length > 0)
            {
                PlayerPrefs.SetString(Names.SelectedLevel, level);
                PlayerPrefs.Save();   
            }

            SceneManager.LoadScene(Names.DefaultLevel);
        }
    }

    public void Setup(string levelName, bool isPremade)
    {
        this.level = levelName;
        this.isPremade = isPremade;

        if (text == null)
            return;
        
        if (isPremade)
        {
            text.text = levelName;
        }
        else
        {
            if (levelName.Length > 0)
            {
                text.text = levelName.Substring(0, levelName.IndexOf(".data", StringComparison.Ordinal));   
            }
        }
    }
}