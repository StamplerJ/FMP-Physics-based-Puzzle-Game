using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OnSaveLevel : MonoBehaviour
{
    [SerializeField] private InputField inputField;

    public void OnClickSaveLevel()
    {
        string levelName = inputField.text;
        if (levelName.Length > 0)
        {
            SaveSystem.Instance.Save(levelName);
            StartCoroutine(BlinkToColor(Color.green, 0.2f));
        }
        else
        {
            StartCoroutine(BlinkToColor(Color.red, 0.2f));
        }
    }
    
    private IEnumerator BlinkToColor (Color toColor, float duration)
    {
        Color startColor = inputField.image.color;
        for (float f = 0; f <= duration; f = f + Time.deltaTime) 
        {
            inputField.image.color = Color.Lerp (inputField.image.color, toColor, f);
            yield return null;
 
        }
        inputField.image.color = toColor;
        for (float f = 0; f <= duration; f = f + Time.deltaTime) 
        {
            inputField.image.color = Color.Lerp (inputField.image.color, startColor, f);
            yield return null;
        }

        inputField.image.color = startColor;
    }
}
