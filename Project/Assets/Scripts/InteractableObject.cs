using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InteractableObject : MonoBehaviour, IPointerDownHandler
{
    #region Static

    public static Action OnInteractableObjectsCountChange = delegate { };

    private static int interactableObjectsCount = 0;
    public static int InteractableObjectsCount
    {
        get
        {
            return interactableObjectsCount;
        }
        set
        {
            interactableObjectsCount = value;
            OnInteractableObjectsCountChange();
        }
    }


    #endregion

    #region Fields

    private ToolsController toolsController;
    private InteractablesRespawner respawner;

    [SerializeField]
    private int oxygenReward = 3;

    [SerializeField]
    private GameObject dialogMessagePrefab;

    private GameObject spawnedCriticalMessage;

    #endregion

    #region Methods

    protected virtual void Awake()
    {
        toolsController = FindObjectOfType<ToolsController>();
    }

    protected virtual void Start()
    {
        InteractableObjectsCount++;

        if (dialogMessagePrefab != null)
            spawnedCriticalMessage = FindObjectOfType<DialogsController>().SpawnCriticalMessage(dialogMessagePrefab);

    }

    private void OnDestroy()
    {
        
    }

    public void SetRespawner(InteractablesRespawner _respawner)
    {
        respawner = _respawner;
    }

    private void OnMouseDown()
    {
        
    }

    protected abstract bool CanInteract(ToolType toolType);

    protected abstract void InteractionBegin();

    public void DestroyInteractable()
    {
        if (spawnedCriticalMessage != null)
        {
            Destroy(spawnedCriticalMessage);
        }

        InteractableObjectsCount--;

        if (respawner != null)
            respawner.RemoveInteractable();

        Destroy(gameObject);



        
    }

    public void InteractionSuccess()
    {
        OxygenManager.Instance.SpawnBalls(oxygenReward);

        TreeHumorManager.Instance.TriggerHappy();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("POINTER DOWN");
        if (toolsController.CurrentTool == null)
            return;

        if (CanInteract(toolsController.CurrentTool.TypeTool))
        {
            FindObjectOfType<ToolCursor>().PlayCurrentToolAnimation();
            InteractionBegin();
        }
    }

    #endregion
}
