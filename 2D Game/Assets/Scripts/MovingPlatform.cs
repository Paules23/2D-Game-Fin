using UnityEngine;

public class MovingPlatform : Platform
{
    public float speed = 2f;
    public float range = 2f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }
    protected override void Update()
    {
        base.Update();
        transform.position = startPosition + new Vector3(Mathf.Sin(Time.time * speed) * range, 0, 0);
    }
}
