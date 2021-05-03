using System.Collections.Generic;
using System.Linq;

public class SelectedItemsTracker : Singleton<SelectedItemsTracker>
{
    private List<Selectable> selectables;

    private void Start()
    {
        Initialise();
    }

    public void Initialise()
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

    public void AddSelectable(Selectable selectable, bool setSelected)
    {
        selectables.Add(selectable);
        selectable.UpdateSelection(setSelected);

        if (setSelected)
        {
            UpdateSelectedItems(selectable);   
        }
    }
}