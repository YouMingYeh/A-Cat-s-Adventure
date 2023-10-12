using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
                Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    GameObject selected = hit.transform.gameObject;
                    SpriteRenderer sprite = selected.GetComponent <SpriteRenderer>();

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
                        playerController.ChangeControlEnability(true);

                    }

                }

            }
        }
    }
}
