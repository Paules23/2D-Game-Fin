using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 8f;
    public float springForce = 4f;
    public float moveSpeed = 5f;
    public bool useAccelerometer = false;
    private bool hasJumpedOnFinish = false;

    private Rigidbody2D rb;
    private Collider2D playerCollider;
    private SceneController sceneController;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<Collider2D>();
        sceneController = FindObjectOfType<SceneController>();
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
            Debug.Log("Game Over :(");
            sceneController.Lost();
            gameObject.SetActive(false);
        }
    }

    private void HandleMovement()
    {
        // Movimiento horizontal
        float horizontalInput = useAccelerometer ? Input.acceleration.x : Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Orientación del personaje
        if (horizontalInput > 0 && transform.localScale.x > 0) // Derecha
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalInput < 0 && transform.localScale.x < 0) // Izquierda
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ScreenEdge"))
        {
            HandleScreenEdge(collision);
        }
        else if (collision.CompareTag("Platform"))
        {
            if (transform.position.y > collision.bounds.max.y && rb.velocity.y <= 0)
            {
                rb.velocity = Vector2.up * jumpForce;
            }
        }
        else if (collision.CompareTag("Spring"))
        {
            if (transform.position.y > collision.bounds.max.y && rb.velocity.y <= 0)
            {
                rb.velocity = Vector2.up * springForce;
            }
        }
        else if (collision.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            Debug.Log("Colisión con enemigo game lost");
            sceneController.Lost();
        }
        else if (collision.CompareTag("Finish"))
        {
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            Debug.Log("Game Over :))");
            sceneController.EndGame();
        }
    }

    private void HandleScreenEdge(Collider2D edgeCollider)
    {
        if (edgeCollider.name == "LeftEdge")
        {
            // Teletransporta al jugador al borde derecho (posición del collider derecho)
            GameObject rightEdge = GameObject.Find("RightEdge");
            if (rightEdge != null)
            {
                transform.position = new Vector2(rightEdge.transform.position.x - 0.35f, transform.position.y);
            }
        }
        else if (edgeCollider.name == "RightEdge")
        {
            // Teletransporta al jugador al borde izquierdo (posición del collider izquierdo)
            GameObject leftEdge = GameObject.Find("LeftEdge");
            if (leftEdge != null)
            {
                transform.position = new Vector2(leftEdge.transform.position.x + 0.35f, transform.position.y);
            }
        }
    }
}
