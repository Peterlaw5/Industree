using System;
using UnityEngine;

public class CriticalStateController : MonoBehaviour
{
    #region Fields

    private bool isDry;
    private bool criticalState = false;

    [SerializeField]
    private int minCriticalItemsCount = 1;

    [SerializeField]
    private GameObject waterCriticalMessagePrefab;

    private GameObject waterCriticalMessage;

    public Action OnCriticalStateEnter = delegate { };
    public Action OnCriticalStateExit = delegate { };

    #endregion

    #region Properties

    public bool IsCriticalState { get { return criticalState; } }

    #endregion

    #region Methods

    private void Awake()
    {
        OnCriticalStateEnter = delegate { };
        OnCriticalStateExit = delegate { };

        InteractableObject.OnInteractableObjectsCountChange = delegate { };
        InteractableObject.InteractableObjectsCount = 0;

        var waterController = FindObjectOfType<WaterController>();

        waterController.OnDry += () =>
        {
            isDry = true;
            CheckCriticalState();
            Debug.Log("Hello");
            waterCriticalMessage = FindObjectOfType<DialogsController>().SpawnCriticalMessage(waterCriticalMessagePrefab);
        };

        waterController.OnEndDry += () =>
        {
            isDry = false;
            CheckCriticalState();

            if (waterCriticalMessage != null)
                Destroy(waterCriticalMessage.gameObject);
        };

        InteractableObject.OnInteractableObjectsCountChange += CheckCriticalState;
    }

    private void CheckCriticalState()
    {
        if (isDry && !criticalState)
        {
            criticalState = true;
            OnCriticalStateEnter();
        }
        if (InteractableObject.InteractableObjectsCount > minCriticalItemsCount && !criticalState)
        {
            criticalState = true;
            OnCriticalStateEnter();
        }

        if (!isDry && InteractableObject.InteractableObjectsCount <= minCriticalItemsCount && criticalState)
        {
            criticalState = false;
            OnCriticalStateExit();
        }

        if (criticalState)
        {
            if (TreeHumorManager.Instance != null)
                TreeHumorManager.Instance.ToggleSad(true);

            if (OxygenManager.Instance != null)
                OxygenManager.Instance.StopAutoOxygen();
        }
        else
        {
            if (TreeHumorManager.Instance != null)
                TreeHumorManager.Instance.ToggleSad(false);

            if (OxygenManager.Instance != null)
                OxygenManager.Instance.PlayAutoOxygen();
        }


    }

    #endregion

}
