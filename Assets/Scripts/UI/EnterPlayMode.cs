using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnterPlayMode : Singleton<EnterPlayMode>
{
    [SerializeField] private Text buttonText;
    private bool isPlayMode;
    
    public void OnButtonClick()
    {
        isPlayMode = !isPlayMode;
        
        if (isPlayMode)
        {
            buttonText.text = "Editor";

            FindObjectsOfType<MechanicBehaviour>().ToList().ForEach(behaviour => behaviour.OnEnterPlayMode());
            
            SelectedItemsTracker.Instance.UpdateSelectedItems(null);
            CameraController.Instance.ShowThirdPerson();
        }
        else
        {
            buttonText.text = "Play";

            FindObjectsOfType<MechanicBehaviour>().ToList().ForEach(behaviour => behaviour.OnEnterEditor());
            
            CameraController.Instance.ShowTopDown();
        }
    }

    public bool IsPlayMode
    {
        get => isPlayMode;
        set => isPlayMode = value;
    }
}
