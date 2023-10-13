using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            if (collision.transform.CompareTag("Level") || collision.transform.CompareTag("Block"))
            {
                Destroy(transform.gameObject);
            }
        }
    }
}
