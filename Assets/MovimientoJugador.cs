using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private AudioSource audioSource;

    [Header("Movement")]
    private float movimientoHorizontal = 0f;
    [SerializeField] private float velocidadDeMovimiento;
    [SerializeField] private float suavizadoDeMovimiento;
    private Vector3 velocidad = Vector3.zero;
    private bool mirandoDerecha = true;

    [Header("Jump")]
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;
    private bool salto = false;

    [SerializeField] private AudioClip saltoSonido;

    [Header("Respawn")]
    [SerializeField] private Vector3 posicionInicial;
    [SerializeField] private float tiempoParpadeo = 2f;
    [SerializeField] private float intervaloParpadeo = 0.2f;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip muerteSonido; // Sound for when the player dies
    [SerializeField] private AudioClip respawnSonido; // Sound for when the player respawns

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        posicionInicial = transform.position;
    }

    private void Update()
    {
        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;

        if (Input.GetButtonDown("Jump"))
        {
            salto = true;
        }
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);
        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
        salto = false;
    }

    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);

        if (mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            Girar();
        }

        if (enSuelo && saltar)
        {
            enSuelo = false;
            rb2D.AddForce(new Vector2(0f, fuerzaDeSalto));
            if (saltoSonido != null)
            {
                audioSource.PlayOneShot(saltoSonido);
            }
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet") || other.CompareTag("Enemigo"))
        {
            if (muerteSonido != null)
            {
                audioSource.PlayOneShot(muerteSonido);
            }
            Respawn();
        }
    }

    private void Respawn()
    {
        StartCoroutine(FlashPlayer());
        transform.position = posicionInicial;
        rb2D.velocity = Vector2.zero;
        if (respawnSonido != null)
        {
            audioSource.PlayOneShot(respawnSonido);
        }
    }

    private System.Collections.IEnumerator FlashPlayer()
    {
        float endTime = Time.time + tiempoParpadeo;
        bool visible = true;

        while (Time.time < endTime)
        {
            spriteRenderer.enabled = visible;
            visible = !visible;
            yield return new WaitForSeconds(intervaloParpadeo);
        }

        spriteRenderer.enabled = true;
    }
}
