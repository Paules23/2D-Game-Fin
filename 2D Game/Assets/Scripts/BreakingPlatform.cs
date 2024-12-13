using UnityEngine;
using System.Collections;

public class BreakingPlatform : Platform
{
    private bool isBreaking = false;
    private Collider2D myCollider;

    void Start()
    {
        myCollider = GetComponent<Collider2D>();
        myCollider.enabled = false;
        myCollider.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.position.y > myCollider.bounds.max.y && other.CompareTag("Player") && !isBreaking)
        {
            Debug.Log("Entered breaking");
            StartCoroutine(BreakPlatform());
        }
    }

    private IEnumerator BreakPlatform()
    {
        Debug.Log("Breaking platform");
        isBreaking = true;
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
