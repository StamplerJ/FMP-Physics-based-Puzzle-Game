using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EnterPlayMode : MonoBehaviour
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
        }
        else
        {
            buttonText.text = "Play";
            FindObjectsOfType<MechanicBehaviour>().ToList().ForEach(behaviour => behaviour.OnEnterEditor());
        }
    }
}
