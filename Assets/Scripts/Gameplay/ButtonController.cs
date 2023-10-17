using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonController : MonoBehaviour
{

    public bool isPressed = false;
    public BoxCollider2D boxCollider;
    public Vector2 StartingPoint;
    public Vector2 EndingPoint;
    public bool biggerOrSmaller = true;
    SpriteAnimator spriteAnimator;
    private void Start()
    {
        spriteAnimator = GetComponent<SpriteAnimator>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (!isPressed)
            {
                spriteAnimator.isReversed = false;
                spriteAnimator.currentFrame = 0;
                spriteAnimator.Play();
            }
            isPressed = true;
            if (biggerOrSmaller)
            {
                if (other.transform.localScale.x < 5.0f)
                {

                    other.transform.localScale = other.transform.localScale + Vector3.one * 0.01f;

                }
            } 
            else
            {
                if (other.transform.localScale.x > 0.3f)
                {

                    other.transform.localScale = other.transform.localScale - Vector3.one * 0.01f;

                }
            }
            
        }
    }

    private void Update()
    {
        if (isPressed)
        {
            if (boxCollider.size.y > 0.02f)
            {
                Vector2 newSize = boxCollider.size;
                newSize.y -= 0.01f;
                boxCollider.size = newSize;
            }
        }
        else
        {
            if (boxCollider.size.y < 0.08f)
            {
                Vector2 newSize = boxCollider.size;
                newSize.y += 0.01f;
                boxCollider.size = newSize;
            } 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.transform.CompareTag("Player"))
        {
            if(isPressed)
            {
                spriteAnimator.currentFrame = 0;
                spriteAnimator.isReversed = true;
                spriteAnimator.Play();
                isPressed = false;
            }
            
        }
    }
}
