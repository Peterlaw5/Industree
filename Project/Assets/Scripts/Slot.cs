using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public void FillSlot(Item item)
    {
        gameObject.SetActive(true);
        
        GetComponent<SpriteRenderer>().sprite = item != null ? item.artwork : null;
        DataManager.Instance.AddItem(item);

    }

    public void EraseSlot(int itemType)
    {
        gameObject.SetActive(false);
        DataManager.Instance.RemoveItem((ItemType)itemType);

    }
}
