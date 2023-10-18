using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndingController : MonoBehaviour
{
    public UnityEvent EndGame;
    public AudioSource audioSource;
    public AudioClip audioClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            audioSource.PlayOneShot(audioClip);
            EndGame?.Invoke();

        }
    }
}
