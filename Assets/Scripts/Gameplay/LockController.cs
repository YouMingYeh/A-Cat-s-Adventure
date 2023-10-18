using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LockController : MonoBehaviour
{
    public KeyController key1;
    public KeyController key2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(key1.isFound || key2.isFound)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
