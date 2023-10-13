using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// This event is fired when user input should be enabled.
    /// </summary>
    public class EnablePlayerInput : Simulation.Event<EnablePlayerInput>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        public PlayerController playerController;
        public override void Execute()
        {
            var player = playerController;
            player.controlEnabled = true;
            UpdateFocus(playerController.gameObject);
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