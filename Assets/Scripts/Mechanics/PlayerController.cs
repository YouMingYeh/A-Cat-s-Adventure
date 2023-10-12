using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 8;

        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/ public Collider2D collider2d;
        /*internal new*/ public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        public int canJumpTime = 2;
        public bool quickFall = false;
        public float quickFallSpeed = 10f;

        public ParticleSystem dust;
        public ParticleSystem landingDust;
        public ParticleSystem landingBurst;
        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
        }

        protected override void Update()
        {

            if (controlEnabled)
            {
                move.x = Input.GetAxis("Horizontal");
                if (Mathf.Abs(move.x) > 0.1f && IsGrounded) { dust.Play(); }

                if (canJumpTime > 0 && Input.GetButtonDown("Jump"))
                {
                    Debug.Log("Jump");
                    jumpState = JumpState.PrepareToJump;
                    dust.Play();    
                }
                else if (Input.GetButtonUp("Jump"))
                {
                    Debug.Log("stopJump");
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
                else if (!IsGrounded && Input.GetButtonDown("QuickFall"))
                {
                    Debug.Log("QUICKFALL");
                    stopJump = true;
                    quickFall = true;
                }
            }
            else
            {
                move.x = 0;
            }
            UpdateJumpState();
            base.Update();
        }

        public void ChangeControlEnability(bool input)
        {
            controlEnabled = input;
        }
        void UpdateJumpState()
        {
            
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    canJumpTime -= 1;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                        if(quickFall == true)
                        {
                            landingBurst.Play();
                        }
                        else
                        {
                            landingDust.Play();
                        }
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    canJumpTime = 2;
                    quickFall = false;
                    
                    break;

            }
        }

        protected override void ComputeVelocity()
        {

            if (jump)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            } 
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
                if (quickFall)
                {
                    velocity.y -= quickFallSpeed;
                    animator.SetTrigger("QuickFall");
                }
            }
            
            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
            

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed,
        }
    }
}