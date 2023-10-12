using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CharacterSelect : MonoBehaviour
{
    // Start is called before the first frame update

    PlatformerModel model;
    void Start()
    {
        model = Simulation.GetModel<PlatformerModel>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 origin = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                                         Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(origin, Vector2.zero, 0f);
            if (hit)
            {
                UpdateFocus(hit.transform.gameObject);
            }
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
                    model.virtualCamera.m_LookAt = selected.transform;
                    model.virtualCamera.m_Follow = selected.transform;
                }


            }

        }
    }
}
