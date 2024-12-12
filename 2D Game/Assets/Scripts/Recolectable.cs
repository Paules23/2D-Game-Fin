using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recolectable : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    private SceneController sceneManager;
    
    // Start is called before the first frame update
    void Start()
    {
        sceneManager = FindObjectOfType<SceneController>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            audioSource.PlayOneShot(audioClip);
            sceneManager.AddPoint();
            // desactivamos visualmente para no cargarnos el sonido primero
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;

            // destruimos el objeto cuando acabe el sonido
            Destroy(gameObject, audioClip.length);
        }
    }

}
