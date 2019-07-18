using System;
using UnityEngine;
using UnityEngine.UI;

public class ToolUI : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Button button;

    [SerializeField]
    private Image icon;

    private Tool targetTool;
    #endregion

    #region Methods

    public void Init(Tool tool)
    {
        targetTool = tool;

        icon.sprite = targetTool.Icon;
    }

    public void ChangeOnClick(Action<Tool> onToolPressed)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => onToolPressed(targetTool));
    }

    #endregion
}
