using Spine.Unity;
using UnityEngine;

[CreateAssetMenu(fileName = "Tool", menuName = "INDUSTREE/Tool")]
public class Tool : ScriptableObject
{
    #region Fields

    [SerializeField]
    private Sprite icon;

    [SerializeField]
    private RuntimeAnimatorController animatorController;
    [SerializeField]
    private SkeletonDataAsset animationDataAsset;

    [SerializeField]
    private ToolType toolType;

    #endregion

    #region Properties

    public Sprite Icon { get { return icon; } }
    public ToolType TypeTool { get { return toolType; } }
    public RuntimeAnimatorController AnimatorController { get { return animatorController; } }
    public SkeletonDataAsset AnimationDataAsset { get { return animationDataAsset; } }

    #endregion

    #region Methods

    public void OnToolDeselected()
    {
    }

    public void OnToolSelected()
    {
    }

    #endregion

}

public enum ToolType
{
    Recycler,
    Watering,
    Remover
}
