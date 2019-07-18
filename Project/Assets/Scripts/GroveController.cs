using UnityEngine;

public class GroveController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private GameObject treeGrovePrefab;

    [SerializeField]
    private float distanceBetweenTrees = 10f;

    [SerializeField]
    private Transform startingSpawnPoint;

    #endregion

    #region Methods

    private void Start()
    {
        LoadTrees();
    }

    private void LoadTrees()
    {
        DataManager dataManager = DataManager.Instance;

        var trees = dataManager.LoadTrees();

        for (int i = 0; i < trees.Count; i++)
        {
            var tree = trees[i];

            GameObject treeClone = Instantiate(treeGrovePrefab, startingSpawnPoint.position + Vector3.right * distanceBetweenTrees * i, Quaternion.identity);

            TreeGroveLoader treeLoader = treeClone.GetComponent<TreeGroveLoader>();
            treeLoader.LoadTree(tree);
        }
    }

    #endregion
}
