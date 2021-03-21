using UnityEngine;

public class Selectable : MonoBehaviour
{
    private GameObject onObjectCanvas;
    private Outline outline;
    
    private bool isSelected = false;

    private void Awake()
    {
        onObjectCanvas = transform.Find(Names.OnObjectCanvas)?.gameObject;

        outline = GetComponent<Outline>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.Equals(this.gameObject))
                {
                    SelectedItemsTracker.Instance.UpdateSelectedItems(this);
                }
            }
        }
    }
    
    public void UpdateSelection(bool isSelected)
    {
        if (onObjectCanvas == null)
            return;
        
        onObjectCanvas.SetActive(isSelected);

        if (outline != null)
        {
            outline.enabled = isSelected;
        }
    }

    public bool IsSelected => isSelected;
}
