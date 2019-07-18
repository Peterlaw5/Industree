#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "INDUSTREE/Item")]
public class Item : ScriptableObject
{
    [SerializeField]
    public string itemName;

    [SerializeField]
    public ItemType type;

    [SerializeField]
    public Sprite artwork;

    [SerializeField]
    private string guid;

    public string GUID { get { return guid; } }

    private void OnValidate()
    {
#if UNITY_EDITOR
        guid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(this));
#endif
    }

}

public enum ItemType
{
    Enclosure,
    GroundL,
    GroundR,
    Glasses,
    Neck,
    Foliage,
    Pendants
}