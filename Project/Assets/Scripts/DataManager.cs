using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class DataManager : MonoBehaviour{

    #region Static

    public static DataManager Instance { get; private set; }

    public const string DEFAULT_TREE_NAME = "Ciro";
    public const string DEFAULT_TREE_GENDER = "maschio";

    public static TreeData<string> currentTreeData = new TreeData<string>();

    #endregion

    #region Fields

    private string dataFileName = "data.bin";
    private string dataPath;

    private SerializableData cachedData;

    public event Action OnDataChange = delegate { };

    #endregion

    #region Methods

    private void Awake()
    {
        Instance = this;
        dataPath = Application.persistentDataPath + dataFileName;
        Load();
    }
    

    private void Load()
    {
        if (File.Exists(dataPath))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = new FileStream(dataPath, FileMode.Open);

            cachedData = formatter.Deserialize(file) as SerializableData;

            file.Close();
        }
        else
        {
            ResetData();
        }
    }

    private void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = new FileStream(dataPath,FileMode.OpenOrCreate);

        formatter.Serialize(file, cachedData);

        file.Close();

        OnDataChange();
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();

        cachedData = new SerializableData();
        Save();
    }

    public void ClearCurrentData()
    {
        currentTreeData =  new TreeData<string>();
    }

    public TreeData<string> AddNewTree(string treeName = DEFAULT_TREE_NAME, string gender = DEFAULT_TREE_GENDER)
    {
        var treeData = new TreeData<string>() { Name = treeName, Gender = gender};

        cachedData.trees.Add(treeData);
        Save();

        return treeData;
    }

    public void AddCurrentTree()
    {
        if (cachedData.trees.Count > 3)
            cachedData.trees.Clear();
        
        currentTreeData.Name = PlayerPrefs.GetString("tree_name", DEFAULT_TREE_NAME);
        currentTreeData.Gender = PlayerPrefs.GetString("gender", DEFAULT_TREE_GENDER);

        cachedData.trees.Add(currentTreeData);
        Save();
    }

    public void AddItem(TreeData<string> treeData, Item item)
    {
        if (item == null || treeData == null)
            return;

        switch (item.type)
        {
            case ItemType.Enclosure:
                treeData.Enclosure = item.GUID;
                break;
            case ItemType.GroundL:
                treeData.GroundL = item.GUID;
                break;
            case ItemType.GroundR:
                treeData.GroundR = item.GUID;
                break;
            case ItemType.Glasses:
                treeData.Glasses = item.GUID;
                break;
            case ItemType.Neck:
                treeData.Neck = item.GUID;
                break;
            case ItemType.Foliage:
                treeData.Foliage = item.GUID;
                break;
            case ItemType.Pendants:
                treeData.Pendants = item.GUID;
                break;
            default:
                break;
        }

        Save();
    }

    public void AddItem(Item item)
    {
        if (item == null)
            return;

        AddItem(currentTreeData, item);
    }

    public void RemoveItem(ItemType itemType)
    {
        RemoveItem(currentTreeData, itemType);
    }

    public void RemoveItem(TreeData<string> treeData, ItemType item)
    {

        switch (item)
        {
            case ItemType.Enclosure:
                treeData.Enclosure = null;
                break;
            case ItemType.GroundL:
                treeData.GroundL = null;
                break;
            case ItemType.GroundR:
                treeData.GroundR = null;
                break;
            case ItemType.Glasses:
                treeData.Glasses = null;
                break;
            case ItemType.Neck:
                treeData.Neck = null;
                break;
            case ItemType.Foliage:
                treeData.Foliage = null;
                break;
            case ItemType.Pendants:
                treeData.Pendants = null;
                break;
            default:
                break;
        }

        Save();
    }

    public List<TreeData<Item>> LoadTrees()
    {
        var trees = new List<TreeData<Item>>();
        foreach (var tree in cachedData.trees)
        {
            TreeData<Item> newTree = new TreeData<Item>()
            {
                Enclosure = Resources.LoadAll<Item>("Items/").Where(x => x.GUID == tree.Enclosure).FirstOrDefault(),
                GroundL = Resources.LoadAll<Item>("Items/").Where(x => x.GUID == tree.GroundL).FirstOrDefault(),
                GroundR = Resources.LoadAll<Item>("Items/").Where(x => x.GUID == tree.GroundR).FirstOrDefault(),
                Glasses = Resources.LoadAll<Item>("Items/").Where(x => x.GUID == tree.Glasses).FirstOrDefault(),
                Neck = Resources.LoadAll<Item>("Items/").Where(x => x.GUID == tree.Neck).FirstOrDefault(),
                Foliage = Resources.LoadAll<Item>("Items/").Where(x => x.GUID == tree.Foliage).FirstOrDefault(),
                Pendants = Resources.LoadAll<Item>("Items/").Where(x => x.GUID == tree.Pendants).FirstOrDefault(),
                Name = tree.Name,
                Gender = tree.Gender
            };

            trees.Add(newTree);
        }

        return trees;
    }

    public static TreeData<Item> GetCurrentTree()
    {
        TreeData<Item> newTree = new TreeData<Item>()
        {
            Enclosure = Resources.LoadAll<Item>("Items/").Where(x => x.GUID == currentTreeData.Enclosure).FirstOrDefault(),
            GroundL = Resources.LoadAll<Item>("Items/").Where(x => x.GUID == currentTreeData.GroundL).FirstOrDefault(),
            GroundR = Resources.LoadAll<Item>("Items/").Where(x => x.GUID == currentTreeData.GroundR).FirstOrDefault(),
            Glasses = Resources.LoadAll<Item>("Items/").Where(x => x.GUID == currentTreeData.Glasses).FirstOrDefault(),
            Neck = Resources.LoadAll<Item>("Items/").Where(x => x.GUID == currentTreeData.Neck).FirstOrDefault(),
            Foliage = Resources.LoadAll<Item>("Items/").Where(x => x.GUID == currentTreeData.Foliage).FirstOrDefault(),
            Pendants = Resources.LoadAll<Item>("Items/").Where(x => x.GUID == currentTreeData.Pendants).FirstOrDefault(),
            Name = currentTreeData.Name,
            Gender = currentTreeData.Gender
        };
        return newTree;
    }

    #endregion

    #region Tests

    
    [ContextMenu("TestAddTreeWithNoAttachements")]
    public void TestAddTreeWithNoAttachements()
    {
        Awake();
        AddNewTree();
    }

    [Header("Tests")]
    [SerializeField] private Item test1Enclosure;
    [SerializeField] private Item test1GroundL;
    [SerializeField] private Item test1GroundR;
    [SerializeField] private Item test1Glasses;
    [SerializeField] private Item test1Neck;
    [SerializeField] private Item test1Foliage;
    [SerializeField] private Item test1Pendants;
    [SerializeField] private string test1TreeName;
    [SerializeField] private string test1TreeGender = DEFAULT_TREE_NAME;
    [Space]
    [SerializeField] private Item test2Enclosure;
    [SerializeField] private Item test2GroundL;
    [SerializeField] private Item test2GroundR;
    [SerializeField] private Item test2Glasses;
    [SerializeField] private Item test2Neck;
    [SerializeField] private Item test2Foliage;
    [SerializeField] private Item test2Pendants;
    [SerializeField] private string test2TreeName;
    [SerializeField] private string test2TreeGender = "femmina";
    [ContextMenu("TestAdd2TreesWithAttachments")]
    public void TestAddMultipleTreesWithAttachments()
    {
        Awake();

        var tree1 = AddNewTree(test1TreeName, test1TreeGender);
        AddItem(tree1, test1Enclosure);
        AddItem(tree1, test1GroundL);
        AddItem(tree1, test1GroundR);
        AddItem(tree1, test1Glasses);
        AddItem(tree1, test1Neck);
        AddItem(tree1, test1Foliage);
        AddItem(tree1, test1Pendants);

        var tree2 = AddNewTree(test2TreeName, test2TreeGender);
        AddItem(tree2, test2Enclosure);
        AddItem(tree2, test2GroundL);
        AddItem(tree2, test2GroundR);
        AddItem(tree2, test2Glasses);
        AddItem(tree2, test2Neck);
        AddItem(tree2, test2Foliage);
        AddItem(tree2, test2Pendants);
    }

    [ContextMenu("TestLoadTrees")]
    public void TestLoadTrees()
    {
        Awake();

        var trees = LoadTrees();

        foreach (var tree in trees)
        {
            Debug.Log("TREE: " + tree.Name);
            Debug.Log("GENDER: " + tree.Gender);

            if (tree.Enclosure != null)
                Debug.Log("Enclosure :" + tree.Enclosure.itemName);
            if (tree.GroundL != null)
                Debug.Log("GroundL :" + tree.GroundL.itemName);
            if (tree.GroundR != null)
                Debug.Log("GroundR :" + tree.GroundR.itemName);
            if (tree.Glasses != null)
                Debug.Log("Glasses :" + tree.Glasses.itemName);
            if(tree.Neck != null)
                Debug.Log("Neck :" + tree.Neck.itemName);
            if (tree.Foliage != null)
                Debug.Log("Foliage :" + tree.Foliage.itemName);
            if (tree.Pendants != null)
                Debug.Log("Pendants :" + tree.Pendants.itemName);
        }
    }

    [ContextMenu("TestResetTrees")]
    public void TestResetTrees()
    {
        Awake();

        ResetData();
    }

    #endregion

    [Serializable]
    private class SerializableData
    {

        public List<TreeData<string>> trees;

        public SerializableData()
        {
            trees = new List<TreeData<string>>();
        }

    }

    [Serializable]
    public class TreeData<T>
    {
        public T Enclosure;
        public T GroundL;
        public T GroundR;
        public T Glasses;
        public T Neck;
        public T Foliage;
        public T Pendants;

        public string Name = DEFAULT_TREE_NAME;
        public string Gender = DEFAULT_TREE_GENDER;
        
    }
}

