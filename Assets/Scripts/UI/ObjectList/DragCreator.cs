using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragCreator : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform viewport;
    private ListItemUI uiItem;

    private GameObject objectInstance;
    private bool isInstantiated = false;
    
    private void Awake()
    {
        viewport = GameObject.Find(Names.Viewport)?.GetComponent<RectTransform>();
        uiItem = GetComponent<ListItemUI>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (viewport != null)
        {
            Vector2 currentMousePosition = eventData.position;

            if (currentMousePosition.x < (viewport.position.x - viewport.rect.width / 2f))
            {
                if (!isInstantiated)
                {
                    RaycastHit hit;
                    Ray ray = CameraController.Instance.GetCamera().ScreenPointToRay(currentMousePosition);
        
                    if (Physics.Raycast(ray, out hit))
                    {
                        InstantiateItemObject(hit.point);
                    }
                }
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        objectInstance = null;
        isInstantiated = false;
    }

    private void InstantiateItemObject(Vector3 position)
    {
        objectInstance = Instantiate(uiItem.Prefab, position, Quaternion.identity);
        objectInstance.GetComponent<SnapToGrid>().IsSelected = true;
        isInstantiated = true;
    }
}
