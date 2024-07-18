using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagTrigger2 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player collides with the flag
        if (other.CompareTag("Player"))
        {
            // Load the next scene (replace "Level2" with the actual name of your next scene)
            SceneManager.LoadScene("Nivel 3");
        }
    }
}
