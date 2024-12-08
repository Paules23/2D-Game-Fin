using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float moveSpeed = 5f;
    public bool useAccelerometer = true;
    public LayerMask platformLayer;

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private float screenWidth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>(); // Obtener el collider del jugador

        // Calcula el ancho de la pantalla en unidades del mundo
        float halfHeight = Camera.main.orthographicSize;
        screenWidth = Camera.main.aspect * halfHeight * 2f;
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleScreenWrap();
    }

    void Update()
    {
        // Detecta si el jugador cae por debajo de la cámara
        if (transform.position.y < Camera.main.transform.position.y - 7)
        {
            Debug.Log("Game Over");
            gameObject.SetActive(false); // Desactiva el objeto del jugador handlear perder
        }
    }

    private void HandleMovement()
    {
        // Movimiento horizontal
        float horizontalInput = useAccelerometer ? Input.acceleration.x : Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    private void HandleScreenWrap()
    {
        // Convierte las coordenadas del jugador al espacio de la cámara
        Vector3 screenPosition = Camera.main.WorldToViewportPoint(transform.position);

        if (screenPosition.x < 0)
        {
            screenPosition.x = 1; // Aparece por la derecha
        }
        else if (screenPosition.x > 1)
        {
            screenPosition.x = 0; // Aparece por la izquierda
        }

        transform.position = Camera.main.ViewportToWorldPoint(screenPosition);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica si el jugador entra en contacto con una plataforma
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            // Analiza si el jugador viene desde arriba
            if (transform.position.y > collision.bounds.max.y && rb.velocity.y <= 0)
            {
                Debug.Log("Rebote desde arriba");
                rb.velocity = Vector2.up * jumpForce;
            }
            else
            {
                Debug.Log("Entró desde abajo o lado, no hace nada");
            }
        }
    }
}
