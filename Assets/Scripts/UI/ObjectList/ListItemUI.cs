using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItemUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Text text;
    private GameObject prefab;
    
    public void Setup(ListItemScriptableObject item)
    {
        image.sprite = item.image;
        text.text = item.name;
        prefab = item.prefab;
    }

    public GameObject Prefab
    {
        get => prefab;
        set => prefab = value;
    }
}
