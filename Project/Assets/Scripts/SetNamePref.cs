using UnityEngine;

public class SetNamePref : MonoBehaviour
{
    public void SetNamePlayerPrefs(string name)
    {
        PlayerPrefs.SetString("tree_name", name);
    }
}