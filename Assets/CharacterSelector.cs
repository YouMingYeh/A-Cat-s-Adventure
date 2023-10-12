using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class CharacterSelector : MonoBehaviour
{
    public Color selectedColor;
    public Color originalColor;
    public PlayerController playerController;

    public bool isSelected = false;

    void Start()
    {
        SetCharacterColor(originalColor);
    }

    void Update()
    {
        // Check for number key input to select characters.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectCharacter(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectCharacter(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectCharacter(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectCharacter(4);
        }
    }

    void SelectCharacter(int characterNumber)
    {
        // Deselect the previously selected character.
        DeselectAllCharacters();

        // Select the character based on the provided characterNumber.
        SetCharacterColor(originalColor);
        playerController.controlEnabled = true;
        isSelected = true;
    }

    void DeselectAllCharacters()
    {
        // Deselect all characters in the scene.
        CharacterSelector[] characterSelectors = FindObjectsOfType<CharacterSelector>();
        foreach (CharacterSelector selector in characterSelectors)
        {
            selector.isSelected = false;
            selector.SetCharacterColor(selector.originalColor);
            selector.playerController.controlEnabled = false;
        }
    }

    void SetCharacterColor(Color color)
    {
        // Change the character's material to the specified one.
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = color;
        }
    }
}
