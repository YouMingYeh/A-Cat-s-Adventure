using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockController : MonoBehaviour
{
    public KeyController key;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(key.isFound)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
