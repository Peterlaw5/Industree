using UnityEngine;

public class AddTreeAndLoadGrove : MonoBehaviour
{
    public void AddTreeAndLoadGroveButton()
    {
        DataManager.Instance.AddCurrentTree();
        GetComponent<MenuManager>().FromMenuToGrove();
    }
}
