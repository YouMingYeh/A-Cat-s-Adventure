using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour
{
    public Transform SpawnPoint;
    private bool isChecked = false;
    public SpriteAnimator animToPlay;
    public SpriteAnimator animToStop;

    public AudioClip audioClip;
    public AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && !isChecked)
        {
            if (collision.transform.CompareTag("Player"))
            {
                Debug.Log("checkpoint checked!");
                audioSource.PlayOneShot(audioClip);
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                var cnt = 0;
                foreach (GameObject player in players)
                {   
                    cnt++;
                    PlayerController playerController = player.GetComponent<PlayerController>();    
                    if(collision.gameObject!= player)
                    {
                        playerController.health.Increment();
                        playerController.Teleport(collision.transform.position + new Vector3(0, cnt, 0));
                        playerController.jumpState = PlayerController.JumpState.InFlight;
                    }
                }
                SpawnPoint.transform.position = collision.transform.position + new Vector3(0, 2, 0);
                isChecked = true;
                animToStop.Stop();
                animToPlay.Play();
            }
        }
    }
}
