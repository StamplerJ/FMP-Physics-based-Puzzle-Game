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
            PlayerPrefs.SetString(Names.SelectedLevel, level);
            PlayerPrefs.Save();
            
            SceneManager.LoadScene(Names.DefaultLevel);
        }
    }

    public void Setup(string levelName, bool isPremade)
    {
        this.level = levelName;
        this.isPremade = isPremade;
        
        if (isPremade)
        {
            text.text = levelName;
        }
        else
        {
            text.text = levelName.Substring(0, levelName.IndexOf(".data", StringComparison.Ordinal));
        }
    }
}