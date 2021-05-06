using UnityEngine;
using UnityEngine.UI;

public class MenuDefeat : Singleton<MenuDefeat>
{
    [SerializeField] private GameObject rootUI;
    
    public void ShowMenu()
    {
        rootUI.SetActive(true);
    }
}
