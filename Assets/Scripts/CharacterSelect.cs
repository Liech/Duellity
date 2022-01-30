using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    public Sprite[] characters;
    public Sprite profilePic;
    public Image charPic;
    public TMP_Text characterInt;
    public int selectedCharacter = 0;
    public void goLeft()
    {
        selectedCharacter--;
        if(selectedCharacter <0)
        {
            selectedCharacter += characters.Length;
        }
        profilePic = characters[selectedCharacter];
        charPic.sprite = profilePic;
    }
    public void goRight()
    {
        selectedCharacter++;
        if (selectedCharacter > characters.Length-1)
        {
            selectedCharacter = 0;
        }
        profilePic = characters[selectedCharacter];
        charPic.sprite = profilePic;
    }
}
