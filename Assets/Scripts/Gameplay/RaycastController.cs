using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Mechanics;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class RaycastController : MonoBehaviour
{
    // Start is called before the first frame update

    PlatformerModel model;
    public Canvas PauseMenu;
    public GameObject SpawnPoint;
    void Start()
    {
        model = Simulation.GetModel<PlatformerModel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !PauseMenu.isActiveAndEnabled)
        {
            Vector2 origin = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                         Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);
            if (hit && model.player.controlEnabled)
            {
                GameObject hitGameObject = hit.transform.gameObject;
                if (hitGameObject != null)
                {
                    if (hitGameObject.CompareTag("Player") && model.player.controlEnabled)
                    {
                        UpdateFocus(hitGameObject);
                    }
                }
            }
        }
    }

    private void CheckAlive()
    {
        
        var player = model.player;
        if (player.health.IsAlive)
        {
            return;
        }
        player.collider2d.enabled = true;
        player.controlEnabled = false;
        if (player.audioSource && player.respawnAudio)
            player.audioSource.PlayOneShot(player.respawnAudio);
        player.health.Increment();
        player.Teleport(model.spawnPoint.transform.position);
        //player.jumpState = PlayerController.JumpState.Grounded;
        player.jumpState = PlayerController.JumpState.InFlight;
        player.animator.SetBool("dead", false);
        model.virtualCamera.m_Follow = player.transform;
        model.virtualCamera.m_LookAt = player.transform;
        Simulation.Schedule<EnablePlayerInput>(2f);
    }
    private void UpdateFocus(GameObject hitGameObject)
    {
        if (hitGameObject.CompareTag("Player"))
        {
            GameObject selected = hitGameObject;
            SpriteRenderer sprite = selected.GetComponent<SpriteRenderer>();

            if (sprite != null)
            {
                GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                Color newColor;
                PlayerController playerController;
                GameObject temp = players[0];
                foreach (GameObject p in players)
                {
                    
                    if (p != null)
                    {
                        playerController = p.GetComponent<PlayerController>();
                        SpriteRenderer othersprites = p.GetComponent<SpriteRenderer>();
                        newColor = othersprites.color; // Create a copy of the current color.
                        newColor.a = 0.5f; // Set the new alpha value.
                        othersprites.color = newColor; // Assign the new color back to the SpriteRenderer.

                        if (playerController != null)
                        {

                            if(playerController.controlEnabled)
                            {
                                //SpawnPoint.transform.position = p.transform.position + new Vector3(0, 1.0f, 0);
                                playerController.ChangeControlEnability(false);
                            }   
                        }
                        temp = p;
                    }
                }

                newColor = sprite.color; // Create a copy of the current color.
                newColor.a = 1.0f; // Set the new alpha value.
                sprite.color = newColor; // Assign the new color back to the SpriteRenderer.
                playerController = selected.GetComponent<PlayerController>();


                if (playerController)
                {
                    playerController.ChangeControlEnability(true);
                    model.player = playerController;
                    model.virtualCamera.m_LookAt = selected.transform;
                    model.virtualCamera.m_Follow = selected.transform;
                    //CheckAlive();
                }


            }

        }
    }
}
