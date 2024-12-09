using UnityEngine;
using System.Collections;

public class MovingPlatform : Platform
{
    public float speed = 2f;
    public float range = 2f;
    public float delay = 0f; // Pau le he añadido un retardo a esto para que no se muevan todas a la vez
    private Vector3 startPosition;
    private bool canMove = false;

    void Start()
    {
        startPosition = transform.position;
    }
    protected override void Update()
    {
        base.Update();
        float adjustedTime = Time.time - delay;
        transform.position = startPosition + new Vector3(Mathf.Sin(adjustedTime * speed) * range, 0, 0);
    }

}
