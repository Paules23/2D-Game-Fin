using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float jumpForce = 8f;
    public float springForce = 4f;
    public float moveSpeed = 5f;
    public bool useAccelerometer = true;
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
        // Detecta si el jugador cae por debajo de la c�mara
        if (transform.position.y < Camera.main.transform.position.y - 7)
        {
            Debug.Log("Game Over :(");
            sceneController.EndGame();
            gameObject.SetActive(false);
            //para probar
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void HandleMovement()
    {
        // Movimiento horizontal
        float horizontalInput = useAccelerometer ? Input.acceleration.x : Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        //Debug.Log("input" + horizontalInput);
        //Orientacion personaje
        if (horizontalInput > 0) // Se mueve a la derecha
        {
            // Asegura que la escala X sea positiva
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
        else if (horizontalInput < 0) // Se mueve a la izquierda
        {
            // Asegura que la escala X sea negativa
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private void WrapAroundScreen()  //ESTO DE MOMENTO NO FUNCIONA
    {
        // Obtener los límites de la cámara
        Camera cam = Camera.main;
        Vector3 screenPosition = cam.WorldToViewportPoint(transform.position);

        // Si el jugador sale por la derecha, aparece por la izquierda
        if (screenPosition.x > 1f)
        {
            Vector3 newPosition = cam.ViewportToWorldPoint(new Vector3(0f, screenPosition.y, screenPosition.z));
            transform.position = new Vector3(newPosition.x, transform.position.y, transform.position.z);
        }
        // Si el jugador sale por la izquierda, aparece por la derecha
        else if (screenPosition.x < 0f)
        {
            Vector3 newPosition = cam.ViewportToWorldPoint(new Vector3(1f, screenPosition.y, screenPosition.z));
            transform.position = new Vector3(newPosition.x, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Verifica el tipo de objeto con el que el jugador colisiona
        if (collision.CompareTag("Platform"))
        {
            // Analiza si el jugador viene desde arriba
            if (transform.position.y > collision.bounds.max.y && rb.velocity.y <= 0)
            {
                //Debug.Log("Rebote desde arriba");
                rb.velocity = Vector2.up * jumpForce;
            }
        }
        else if (collision.CompareTag("Spring"))
        {
            // colision muelle desde arriba igual que la plataforma
            if (transform.position.y > collision.bounds.max.y && rb.velocity.y <= 0)
            {
                //Debug.Log("Rebote desde muelle");
                rb.velocity = Vector2.up * springForce;
            }
        }
        else if (collision.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
            Debug.Log("Colisi�n con enemigo game lost");

            //para probar
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (collision.CompareTag("Finish"))
        {
            rb.velocity = Vector2.zero; // Detiene el movimiento actual
            rb.isKinematic = true;
            Debug.Log("Game Over :))");  
            sceneController.EndGame();     
        }
    }
}
