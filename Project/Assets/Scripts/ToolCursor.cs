using UnityEngine;
using Spine.Unity;
public class ToolCursor : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer gfx;

    [SerializeField]
    private Tool defaultTool;

    [SerializeField]
    private Animator gfxAnimator;

    [SerializeField]
    private SkeletonAnimator gfxSkeletonAnimator;

    private void Start()
    {
        ChangeToolCursor(defaultTool);

        var toolsController = FindObjectOfType<ToolsController>();

        if (toolsController != null)
        {
            FindObjectOfType<ToolsController>().ChangeCurrentTool(defaultTool);
            FindObjectOfType<ToolsController>().OnToolChanged += ChangeToolCursor;
        }
    }

    private void ChangeToolCursor(Tool tool)
    {
        if (tool != null)
        {
            if (gfxAnimator != null)
            {
                gfxAnimator.runtimeAnimatorController = tool.AnimatorController;
                gfxSkeletonAnimator.skeletonDataAsset = tool.AnimationDataAsset;
                gfxSkeletonAnimator.Initialize(true);
            }

            if (gfxAnimator == null || gfxSkeletonAnimator == null || tool.AnimationDataAsset == null || tool.AnimationDataAsset == null)
                gfx.sprite = tool.Icon;
            else
                gfx.sprite = null;

            Cursor.visible = false;
        }
        else
        {
            gfx.sprite = null;
            gfxAnimator.runtimeAnimatorController = null;
            gfxSkeletonAnimator.skeletonDataAsset = null;
            gfxSkeletonAnimator.Initialize(true);

            Cursor.visible = true;

        }
    }

    public void PlayCurrentToolAnimation()
    {
        if (gfxAnimator != null)
            gfxAnimator.SetTrigger("useTool");
    }
}
