using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// DeathZone components mark a collider which will schedule a
    /// PlayerEnteredDeathZone event when the player enters the trigger.
    /// </summary>
    public class DeathZone : MonoBehaviour
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        void OnTriggerEnter2D(Collider2D collider)
        {
            GameObject collidedWith = collider.gameObject;
            PlayerController playerController = collidedWith.GetComponent<PlayerController>();
            Transform playerTransform = collidedWith.GetComponent<Transform>();
            
            if (playerController != null && collidedWith != null)
            {
                UpdateFocus(collidedWith);
                var ev = Schedule<PlayerEnteredDeathZone>();
                ev.deathzone = this;
            }
        }
        private void UpdateFocus(GameObject hitGameObject)
        {
            Debug.Log(hitGameObject.name);
            if (hitGameObject.CompareTag("Player"))
            {
                GameObject selected = hitGameObject;
                SpriteRenderer sprite = selected.GetComponent<SpriteRenderer>();

                if (sprite != null)
                {
                    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
                    Color newColor;
                    PlayerController playerController;
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
                                Debug.Log("Changing control for player: " + playerController.gameObject.name);
                                playerController.ChangeControlEnability(false);
                            }

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
                    }


                }

            }
        }
    }


}