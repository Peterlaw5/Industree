using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GroveButton : MonoBehaviour
{
    private void Start()
    {
        Button button = GetComponent<Button>();
        button.interactable = DataManager.Instance.LoadTrees().Count > 0;
    }
}