using UnityEngine;

public class SetGenderPref : MonoBehaviour
{
    [SerializeField]
    private string genderName = DataManager.DEFAULT_TREE_NAME;
    public void SetGenderPrefToggle(bool selected)
    {
        if (selected)
        {
            PlayerPrefs.SetString("gender", genderName);
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetString("gender"));
        }

    }

}
