using System.Collections.Generic;
using UnityEngine;

public class ObjectList : MonoBehaviour
{
    [SerializeField] private RectTransform content;

    [SerializeField] private GameObject listItemPrefab;
    [SerializeField] private List<ListItemScriptableObject> listItems;

    [SerializeField] private Vector2 itemSize;
    
    private void Start()
    {
        //setContent Holder Height;
        content.sizeDelta = new Vector2(0, listItems.Count * itemSize.y);
        
        for (int i = 0; i < listItems.Count; i++)
        {
            float spawnY = i * itemSize.y;
            Vector3 pos = new Vector3(0, -spawnY, 0);
            
            GameObject spawnedItem = Instantiate(listItemPrefab, pos, Quaternion.identity);
            spawnedItem.transform.SetParent(content, false);

            ListItemUI ui = spawnedItem.GetComponent<ListItemUI>();
            ui.Setup(listItems[i]);
        }
    }
}