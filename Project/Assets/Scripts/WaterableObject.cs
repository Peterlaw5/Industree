using UnityEngine;

public class WaterableObject : InteractableObject
{
    #region Fields

    [SerializeField]
    private float waterIncreaseAmount = 0.2f;

    #endregion

    #region Methods

    protected override bool CanInteract(ToolType toolType)
    {
        return toolType == ToolType.Watering;
    }

    protected override void InteractionBegin()
    {
        FindObjectOfType<WaterController>().IncreaseWaterlevel(waterIncreaseAmount);
    }

    #endregion

}
