using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreePhaseLoader : MonoBehaviour
{
    #region Fields

    [SerializeField] private Slot enclosure;
    [SerializeField] private Slot groundL;
    [SerializeField] private Slot groundR;
    [SerializeField] private Slot glasses;
    [SerializeField] private Slot neck;
    [SerializeField] private Slot foliage;
    [SerializeField] private Slot pendants;

    #endregion

    #region Methods

    private void Start()
    {
        LoadTree(DataManager.GetCurrentTree());
    }

    public void LoadTree(DataManager.TreeData<Item> treeData)
    {
        enclosure.FillSlot(treeData.Enclosure);

        groundL.FillSlot(treeData.GroundL);

        groundR.FillSlot(treeData.GroundR);

        glasses.FillSlot(treeData.Glasses);

        neck.FillSlot(treeData.Neck);

        foliage.FillSlot(treeData.Foliage);

        pendants.FillSlot(treeData.Pendants);
    }

    #endregion
}
