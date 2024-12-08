using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (player != null)
        {
            // Mueve la cámara solo en el eje Y siguiendo al jugador
            if (player.position.y > transform.position.y)
            {
                transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
            }
        }
    }
}
