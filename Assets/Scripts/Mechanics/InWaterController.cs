using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InWaterController : MonoBehaviour
{
    // Start is called before the first frame update
    PlatformerModel model = Simulation.GetModel<PlatformerModel>();
    public float inWaterMaxSpeed = 1.5f;
    public float inWaterJumpTakeOffSpeed = 3f;
    public float inWaterQuickFallSpeed = 5f;
    public float inWaterGravity = 0.1f;
    private float runTimeMaxSpeed;
    private float runTimeJumpTakeOffSpeed;
    private float runTimeQuickFallSpeed;
    private float runTimeGravity;
    private void Start()
    {
        runTimeMaxSpeed = model.player.maxSpeed;
        runTimeJumpTakeOffSpeed = model.player.jumpTakeOffSpeed;
        runTimeQuickFallSpeed = model.player.quickFallSpeed;
        runTimeGravity = model.player.gravityModifier;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.maxSpeed = inWaterMaxSpeed;
            playerController.jumpTakeOffSpeed =  inWaterJumpTakeOffSpeed;
            playerController.quickFallSpeed = inWaterQuickFallSpeed;
            playerController.gravityModifier = inWaterGravity;
            playerController.landingBurst.Play();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            playerController.maxSpeed = runTimeMaxSpeed;
            playerController.jumpTakeOffSpeed = runTimeJumpTakeOffSpeed;
            playerController.quickFallSpeed = runTimeQuickFallSpeed;
            playerController.gravityModifier = runTimeGravity;
        }
    }
}
