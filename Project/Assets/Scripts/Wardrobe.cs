using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Wardrobe : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image armadio;

    public Sprite[] panels;

    public GameObject[] buttons;

    public bool stopTime = true;

    public void SetPanel(int num)
    {
        armadio.GetComponent<Image>().sprite = panels[num - 1];

        for (int i = 0 ; i < buttons.Length ; ++i)
        {
            buttons[i].gameObject.SetActive(false);
        }

        buttons[num - 1].gameObject.SetActive(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(stopTime)
            Time.timeScale = 0;
        
        GetComponent<Animator>().SetBool("isVisible", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(stopTime)
            Time.timeScale = 1;

        GetComponent<Animator>().SetBool("isVisible", false);
    }

}
