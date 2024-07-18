using UnityEngine;

public class Enemigo1 : MonoBehaviour
{
    private AudioSource audioSource;
    [Header("Sound Effects")]
    [SerializeField] private AudioClip muerteSonido; // Sound for when the player dies
    [SerializeField] private AudioClip respawnSonido; // Sound for when the player respawns

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            MovimientoJugador player = other.GetComponent<MovimientoJugador>();
            if (player != null)
            {
                if (muerteSonido != null)
                {
                    audioSource.PlayOneShot(muerteSonido);
                }
            }
        }
    }
}
