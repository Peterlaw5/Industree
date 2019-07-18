using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShow : MonoBehaviour
{
    public Slot enclosure;
    public Slot groundL;
    public Slot groundR;
    public Slot glasses;

    public void SetItem(Item item)
    {
        switch (item.type)
        {
            case ItemType.GroundL:
                groundL.FillSlot(item);
                break;

            case ItemType.GroundR:
                groundR.FillSlot(item);
                break;

            case ItemType.Glasses:
                glasses.FillSlot(item);
                break;

            case ItemType.Enclosure:
                enclosure.FillSlot(item);
                break;
        }
        DataManager.Instance.AddItem(item);
    }
}
