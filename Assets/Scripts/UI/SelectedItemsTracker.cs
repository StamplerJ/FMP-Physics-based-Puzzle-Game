using System.Linq;

public class SelectedItemsTracker : Singleton<SelectedItemsTracker>
{
    private Selectable[] selectables;
    
    void Start()
    {
        selectables = FindObjectsOfType<Selectable>();
        
        // Default all to false
        UpdateSelectedItems(null);
    }

    public void UpdateSelectedItems(Selectable selectable)
    {
        foreach (Selectable item in selectables)
        {
            item.UpdateSelection(item == selectable);
        }
    }

    public Selectable GetSelectable()
    {
        return selectables.FirstOrDefault(item => item.IsSelected);
    }
}
