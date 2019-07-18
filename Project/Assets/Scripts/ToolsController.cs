using System;
using System.Collections.Generic;
using UnityEngine;

public class ToolsController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private List<Tool> tools;

    [SerializeField]
    private ToolsControllerUI controllerUI;

    [SerializeField]
    private AudioSource changeTool;

    public Action<Tool> OnToolChanged = delegate { };

    #endregion

    #region Properties

    public Tool CurrentTool { get; set; }

    #endregion

    #region Methods

    private void Start()
    {
        controllerUI.InstantiateToolsUI(tools);
        controllerUI.OnToolUIClick = ChangeCurrentTool;
    }

    public void ChangeCurrentTool(Tool newTool)
    {
        if (CurrentTool != null)
        {
            CurrentTool.OnToolDeselected();
        }

        CurrentTool = newTool;

        if (changeTool !=null)
            changeTool.Play();
        
        if(CurrentTool != null)
            CurrentTool.OnToolSelected();

        if (CurrentTool != null)
        {
            var waterables = FindObjectsOfType<WaterableObject>();

            foreach (var waterable in waterables)
            {
                waterable.GetComponent<Collider2D>().enabled = (CurrentTool.TypeTool == ToolType.Watering ? true : false);
            }
        }

        OnToolChanged(CurrentTool);
    }

    #endregion
}
