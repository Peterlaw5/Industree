using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RecycleBin : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Button button;

    [SerializeField]
    private EventTrigger eventTrigger;

    [SerializeField]
    private RecyclableType recyclableType;

    #endregion

    #region Methods

    public void Init(RecyclerController recyclerController)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.Drop;
        entry.callback.AddListener((eventData) => { recyclerController.Recycle(recyclableType); });
        eventTrigger.triggers.Add(entry);
    }

    #endregion
}

