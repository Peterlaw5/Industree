using UnityEngine;

public class RecyclableObject : InteractableObject
{
    #region Fields
    
    private RecyclerController recyclerController;

    [SerializeField]
    private RecyclableType recyclableType;

    #endregion

    #region Properties

    public RecyclableType TypeRecyclable { get { return recyclableType; } }

    public GameObject MyGameObject => gameObject;

    #endregion

    #region Methods

    protected override void Awake()
    {
        base.Awake();
        recyclerController = FindObjectOfType<RecyclerController>();
    }

    protected override bool CanInteract(ToolType toolType)
    {
        return toolType == ToolType.Recycler;
    }

    protected override void InteractionBegin()
    {
        Debug.Log("open recycler");
        recyclerController.OpenRecycler(this);
    }

    private void OnMouseUp()
    {
       // recyclerController.CloseRecycler();
    }

    #endregion

}

public enum RecyclableType
{
    plastic,
    glass,
    paper
}
