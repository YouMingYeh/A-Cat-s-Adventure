using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] animationSprites;
    public float frameRate = 10f;
    public bool loop = true;
    public int loopFromFrame = 0;
    public bool isPlaying = false;
    public bool isReversed = false;
    public int currentFrame = 0;
    private float timer = 0f;

    private Sprite[] originalSprites;
    private Sprite[] reversedSprites;


    private void Start()
    {
        if (spriteRenderer == null || animationSprites.Length == 0)
        {
            Debug.LogError("SpriteRenderer or animation sprites not set!");
            isPlaying = false;
        }
        originalSprites = animationSprites;
        reversedSprites = new Sprite[animationSprites.Length];
        Array.Copy(originalSprites, reversedSprites, originalSprites.Length);
        Array.Reverse(reversedSprites);
    }

    private void Update()
    {
        Sprite[] sprites = isReversed ? reversedSprites : originalSprites;
        if (isPlaying && sprites.Length > 0)
        {
            timer += Time.deltaTime;
            float frameDuration = 1f / frameRate;

            if (timer >= frameDuration)
            {
                timer = 0f;
                currentFrame++;

                if (currentFrame >= sprites.Length)
                {
                    if (loop)
                    {
                        currentFrame = loopFromFrame;
                    }
                    else
                    {
                        
                        isPlaying = false;
                    }
                }
                if(currentFrame < sprites.Length)
                    spriteRenderer.sprite = sprites[currentFrame];
            }
            
        }
    }

    // Start playing the animation.
    public void Play()
    {
        isPlaying = true;
    }

    // Pause the animation.
    public void Pause()
    {
        isPlaying = false;
    }

    // Stop the animation and reset to the first frame.
    public void Stop()
    {
        isPlaying = false;
        currentFrame = 0;
        spriteRenderer.sprite = animationSprites[currentFrame];
    }

    // Set the animation speed.
    public void SetSpeed(float newSpeed)
    {
        frameRate = newSpeed;
    }

    // Set whether the animation should loop or not.
    public void SetLoop(bool shouldLoop)
    {
        loop = shouldLoop;
    }
}
