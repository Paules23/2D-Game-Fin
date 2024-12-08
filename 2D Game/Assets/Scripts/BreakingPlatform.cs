using UnityEngine;
using System.Collections;

public class BreakingPlatform : Platform
{
    private bool isBreaking = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered trigger area");

        if (other.CompareTag("Player") && !isBreaking)
        {
            StartCoroutine(BreakPlatform());
        }
    }

    private IEnumerator BreakPlatform()
    {
        Debug.Log("Breaking platform");
        isBreaking = true;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
