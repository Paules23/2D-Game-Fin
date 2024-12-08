using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 8f;
    public float springForce = 20f;
    public float moveSpeed = 5f;
    public bool useAccelerometer = true;

    private Rigidbody2D rb;
    private Collider2D playerCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }

    void Update()
    {
        // Detecta si el jugador cae por debajo de la cámara
        if (transform.position.y < Camera.main.transform.position.y - 7)
        {
            Debug.Log("Game Over");
            gameObject.SetActive(false);
        }
    }

    private void HandleMovement()
    {
        // Movimiento horizontal
        float horizontalInput = useAccelerometer ? Input.acceleration.x : Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica el tipo de objeto con el que el jugador colisiona
        if (collision.CompareTag("Platform"))
        {
            // Analiza si el jugador viene desde arriba
            if (transform.position.y > collision.bounds.max.y && rb.velocity.y <= 0)
            {
                Debug.Log("Rebote desde arriba");
                rb.velocity = Vector2.up * jumpForce;
            }
        }
        else if (collision.CompareTag("Spring"))
        {
            // colision muelle desde arriba igual que la plataforma
            if (transform.position.y > collision.bounds.max.y && rb.velocity.y <= 0)
            {
                Debug.Log("Rebote desde muelle");
                rb.velocity = Vector2.up * springForce;
            }
        }
        else if (collision.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            Debug.Log("Colisión con enemigo game lost");
        }
    }
}
