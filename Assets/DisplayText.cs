using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public Text uiText; // Reference to the Text component in the UI

    void Start()
    {
        if (uiText != null)
        {
            uiText.text = "Hello World"; // Set the text to "Hello World"
        }
        else
        {
            Debug.LogError("Text component is not assigned.");
        }
    }
}
