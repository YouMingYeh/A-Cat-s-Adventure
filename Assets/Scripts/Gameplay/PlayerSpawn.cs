using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player is spawned after dying.
    /// </summary>
    public class PlayerSpawn : Simulation.Event<PlayerSpawn>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        public PlayerController playerController;
        
        public override void Execute()
        {
            var player = playerController;

            if(player.health.IsAlive ) { return; }
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
            
            var ev = Simulation.Schedule<EnablePlayerInput>(2f);
            ev.playerController = playerController;
            //CheckOthers();
        }

       
        private void CheckOthers()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            PlayerController playerController;
            foreach (GameObject p in players)
            {

                if (p != null && p!=model.player)
                {

                    playerController = p.GetComponent<PlayerController>();
                    if(!playerController.health.IsAlive)
                    {
                        model.player = playerController;
                        model.virtualCamera.m_LookAt = p.transform;
                        model.virtualCamera.m_Follow = p.transform;
                        Simulation.Schedule<PlayerSpawn>(3f);
                    }
                }
            }
        }
    }


}