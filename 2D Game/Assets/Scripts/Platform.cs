using UnityEngine;

public class Platform : MonoBehaviour
{
    protected virtual void Update()
    {
        // Obtiene la posición del límite inferior de la cámara
        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize -0.2f;

        // Desactiva la plataforma si está fuera del rango de visión de la cámara
        if (transform.position.y < cameraBottom)
        {
            gameObject.SetActive(false);
        }
    }
}
