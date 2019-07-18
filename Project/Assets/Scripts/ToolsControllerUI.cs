using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolsControllerUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    #region Fields

    [SerializeField]
    private EventTrigger eventTrigger;

    [SerializeField]
    private Transform toolsBar;

    [SerializeField]
    private ToolUI toolUIPrefab;

    [SerializeField]
    private List<Transform> slots = new List<Transform>();

    private List<ToolUI> toolsUI = new List<ToolUI>();

    private Action<Tool> _onToolUIClick = delegate { };

    #endregion

    #region Properties

    public Action<Tool> OnToolUIClick
    {
        get
        {
            return _onToolUIClick;
        }
        set
        {
            _onToolUIClick = value;

            foreach (var toolUI in toolsUI)
            {
                toolUI.ChangeOnClick(_onToolUIClick);
            }
        }
    }

    #endregion

    #region Methods

    public void InstantiateToolsUI(List<Tool> tools)
    {
        for (int i = 0; i < tools.Count; i++)
        {
            ToolUI toolUI = Instantiate(toolUIPrefab, slots[i]);
            toolUI.Init(tools[i]);

            toolsUI.Add(toolUI);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Animator>().SetBool("isVisible", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Animator>().SetBool("isVisible", false);
    }



    #endregion
}
