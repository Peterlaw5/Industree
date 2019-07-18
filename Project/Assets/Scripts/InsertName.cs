using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InsertName : MonoBehaviour
{
    public Button Play;

    public void CheckNameRestriction(string name)
    {
        if (name.Length >= 1)
        {
            if (name[0] != ' ')
            {
                Play.interactable = true;
            }
        }
        else
        {
            Play.interactable = false;
        }
    }
}