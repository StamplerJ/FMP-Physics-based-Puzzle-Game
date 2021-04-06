using System.Collections.Generic;
using System.Linq;

public class SelectedItemsTracker : Singleton<SelectedItemsTracker>
{
    private List<Selectable> selectables;
    
    void Start()
    {
        selectables = FindObjectsOfType<Selectable>().ToList();
        
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

    public void AddSelectable(Selectable selectable)
    {
        selectables.Add(selectable);
        selectable.UpdateSelection(false);
    }
}
