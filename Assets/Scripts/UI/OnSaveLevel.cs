using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OnSaveLevel : MonoBehaviour
{
    [SerializeField] private InputField inputField;
    [SerializeField] private GameObject toast;
    
    public void OnClickSaveLevel()
    {
        string levelName = inputField.text;
        if (levelName.Length > 0 && MenuVictory.Instance.IsLevelPerfectOnce)
        {
            SaveSystem.Instance.Save(levelName);
            StartCoroutine(BlinkToColor(Color.green, 0.2f));
        }
        else
        {
            StartCoroutine(BlinkToColor(Color.red, 0.2f));

            if (!MenuVictory.Instance.IsLevelPerfectOnce)
            {
                StartCoroutine(ShowToast(1f));
            }
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
    
    private IEnumerator ShowToast(float duration)
    {
        RectTransform toastRectTransform = toast.GetComponent<RectTransform>();
        Vector2 startPositon = toastRectTransform.anchoredPosition;
        Vector2 endPositon = new Vector2(startPositon.x, 0f);

        for (float f = 0; f <= duration; f = f + Time.deltaTime) 
        {
            toastRectTransform.anchoredPosition = Vector2.Lerp(startPositon, endPositon, f);
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        
        for (float f = 0; f <= duration; f = f + Time.deltaTime) 
        {
            toastRectTransform.anchoredPosition = Vector2.Lerp(endPositon, startPositon, f);
            yield return null;
        }

        toastRectTransform.anchoredPosition = startPositon;
    }
}
