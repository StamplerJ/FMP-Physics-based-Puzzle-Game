using UnityEngine;
using Utils;

public class Selectable : MonoBehaviour
{
    private GameObject onObjectCanvas;
    private Outline outline;

    private ISelectionListener listener;

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
        if (outline != null)
        {
            outline.enabled = isSelected;
        }

        if (onObjectCanvas != null)
        {
            onObjectCanvas.SetActive(isSelected);
        }

        if (listener != null)
        {
            listener.OnSelected();
        }
    }

    public bool IsSelected => isSelected;
}