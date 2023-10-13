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
                
                var ev = Schedule<PlayerEnteredDeathZone>();
                ev.deathzone = this;
                ev.playerController = playerController;
            }
        }

        

    }
}