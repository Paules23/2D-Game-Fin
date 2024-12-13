using UnityEngine;
using System.Collections;

public class BreakingPlatform : Platform
{
    private bool isBreaking = false;
    private Collider2D myCollider;

    void Start()
    {
        myCollider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //perdon por este if pero me da pereza pensar como hacerlo mas legible
        if (other.transform.position.y > myCollider.bounds.max.y && other.attachedRigidbody.velocity.y <= 0
            && other.CompareTag("Player") && !isBreaking)
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
