using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    
    [SerializeField] private RectTransform premadeContent;
    [SerializeField] private RectTransform customContent;
    
    [SerializeField] private Vector2 itemSize;
    
    private List<string> premadeLevels;
    private List<string> customLevels;

    private void Start()
    {
        premadeLevels = new List<string>{"Level 1", "Level 2", "Level 3", "Level 4", "Level 5"};
        customLevels = SaveSystem.Instance.GetCustomLevels();

        SetupScene(premadeContent, premadeLevels, true);
        SetupScene(customContent, customLevels, false);
    }

    private void SetupScene(RectTransform parent, List<string> levels, bool isPremade)
    {
        //setContent Holder Height;
        parent.sizeDelta = new Vector2(0, levels.Count * itemSize.y);
        
        for (int i = 0; i < levels.Count; i++)
        {
            float spawnY = i * itemSize.y;
            Vector3 pos = new Vector3(0, -spawnY, 0);
            
            GameObject spawnedItem = Instantiate(buttonPrefab, pos, Quaternion.identity);
            spawnedItem.transform.SetParent(parent, false);

            LoadLevel loadLevel = spawnedItem.GetComponent<LoadLevel>();
            loadLevel.Setup(levels[i], isPremade);
        }
    }
}
