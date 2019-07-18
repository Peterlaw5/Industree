using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TreeGroveLoader : MonoBehaviour
{
    #region Fields

    [SerializeField] private Slot enclosure;
    [SerializeField] private Slot groundL;
    [SerializeField] private Slot groundR;
    [SerializeField] private Slot glasses;
    [SerializeField] private Slot neck;
    [SerializeField] private Slot foliage;
    [SerializeField] private Slot pendants;
    [SerializeField] private TextMeshProUGUI treeNameText;

    #endregion

    #region Methods

    public void LoadTree(DataManager.TreeData<Item> treeData)
    {
        enclosure.FillSlot(treeData.Enclosure);

        groundL.FillSlot(treeData.GroundL);

        groundR.FillSlot(treeData.GroundR);

        glasses.FillSlot(treeData.Glasses);

        neck.FillSlot(treeData.Neck);

        foliage.FillSlot(treeData.Foliage);

        pendants.FillSlot(treeData.Pendants);

        treeNameText.text = treeData.Name;

        GenderSelector genderSelector = GetComponent<GenderSelector>();
        if(genderSelector != null)
            genderSelector.SetGender(treeData.Gender);
    }

    #endregion
}
