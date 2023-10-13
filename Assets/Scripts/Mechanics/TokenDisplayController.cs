using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TokenDisplayController : MonoBehaviour
{
    TMP_Text text;
    TokenController tokenController;
    private void Start()
    {
        tokenController = GameObject.Find("GameController").GetComponent<TokenController>();
        text = GetComponent<TMP_Text>();
    }
    void Update()
    {
        if (tokenController && text)
        {
            text.text = (tokenController.tokenCollected.ToString());
        }
    }
}
