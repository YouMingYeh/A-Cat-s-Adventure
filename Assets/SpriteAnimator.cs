using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAnimator : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] animationSprites;
    public float frameRate = 10f;
    public bool loop = true;
    public int loopFromFrame = 0;
    public bool isPlaying = false;

    private int currentFrame = 0;
    private float timer = 0f;
    
   

    private void Start()
    {
        if (spriteRenderer == null || animationSprites.Length == 0)
        {
            Debug.LogError("SpriteRenderer or animation sprites not set!");
            isPlaying = false;
        }
    }

    private void Update()
    {
        if (isPlaying && animationSprites.Length > 0)
        {
            timer += Time.deltaTime;
            float frameDuration = 1f / frameRate;

            if (timer >= frameDuration)
            {
                timer = 0f;
                currentFrame++;

                if (currentFrame >= animationSprites.Length)
                {
                    if (loop)
                    {
                        currentFrame = loopFromFrame;
                    }
                    else
                    {
                        isPlaying = false;
                        // You can add a callback or perform some action when the animation ends here.
                    }
                }

                spriteRenderer.sprite = animationSprites[currentFrame];
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
