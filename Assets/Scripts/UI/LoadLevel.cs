using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public string level;

    public void OnLoadLevel()
    {
        SceneManager.LoadScene(level);
    }
}