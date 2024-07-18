using UnityEngine;
using TMPro; // Use this if you're using TextMeshPro

public class FadeText : MonoBehaviour
{
    public float fadeDuration = 3f; // Duration to fade out
    private TMP_Text uiText; // Reference to the TextMeshPro component

    void Start()
    {
        uiText = GetComponent<TMP_Text>(); // Get the TextMeshPro component

        if (uiText != null)
        {
            StartCoroutine(FadeOutText()); // Start fading the text
        }
        else
        {
            Debug.LogError("TextMeshPro component is not attached to this GameObject.");
        }
    }

    private System.Collections.IEnumerator FadeOutText()
    {
        Color originalColor = uiText.color; // Store the original color
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            uiText.color = Color.Lerp(originalColor, Color.clear, elapsedTime / fadeDuration);
            yield return null; // Wait for the next frame
        }

        uiText.color = Color.clear; // Ensure the text is fully transparent
    }
}
