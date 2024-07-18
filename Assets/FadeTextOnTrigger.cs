using UnityEngine;

public class FadeTextOnTrigger : MonoBehaviour
{
    public Animator textAnimator;
    public AudioSource audioSource;
    public AudioClip fadeOutSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Trigger the fade-out animation
            textAnimator.SetTrigger("Fade");

            // Play the fade-out sound
            if (audioSource != null && fadeOutSound != null)
            {
                audioSource.PlayOneShot(fadeOutSound);
            }
        }
    }
}
