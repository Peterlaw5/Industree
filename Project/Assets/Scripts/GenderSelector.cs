using UnityEngine;
using Spine.Unity;

public class GenderSelector : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private SkeletonAnimator treeSkeleton;

    [SerializeField]
    private bool loadFromPlayerPrefsOnStart = true;

    #endregion

    #region Methods

    private void Start()
    {
        if(loadFromPlayerPrefsOnStart)
            SetGender(PlayerPrefs.GetString("gender", DataManager.DEFAULT_TREE_GENDER));
    }

    public void SetGender(string gender)
    {
        treeSkeleton.skeleton.SetSkin(gender);
    }

    #endregion

}
