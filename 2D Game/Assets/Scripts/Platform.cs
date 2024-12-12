using UnityEngine;

public class Platform : MonoBehaviour
{
    
    protected virtual void Update()
    {
        // Obtiene la posici�n del l�mite inferior de la c�mara
        float cameraBottom = Camera.main.transform.position.y - Camera.main.orthographicSize -0.2f;

        // Desactiva la plataforma si est� fuera del rango de visi�n de la c�mara
        if (transform.position.y < cameraBottom)
        {
            gameObject.SetActive(false);
        }
    }

}
