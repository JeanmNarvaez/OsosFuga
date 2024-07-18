using UnityEngine;
using UnityEngine.UI;

public class TextFader : MonoBehaviour
{
    public Text text; // Make the text field public
    public float fadeDuration = 2f;

    private void Start()
    {
        if (text == null)
        {
            text = GetComponent<Text>();
        }
        text.text = "Hello World";
        StartCoroutine(FadeOutText());
    }

    private System.Collections.IEnumerator FadeOutText()
    {
        Color originalColor = text.color;
        for (float t = 0.01f; t < fadeDuration; t += Time.deltaTime)
        {
            text.color = Color.Lerp(originalColor, Color.clear, Mathf.Min(1, t / fadeDuration));
            yield return null;
        }
        text.color = Color.clear;
    }
}
