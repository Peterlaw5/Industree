using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler,  IEndDragHandler, IDragHandler, IPointerUpHandler
{
    #region Fields

    private Rigidbody2D rb;
    private Vector2 dragPointOffset;
    private ToolsController toolsController;

    private bool draggable;

    #endregion

    #region Methods

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        toolsController = FindObjectOfType<ToolsController>();

        toolsController.OnToolChanged += RefreshDraggable;
        RefreshDraggable(toolsController.CurrentTool);

    }

    private void OnDestroy()
    {
        if(toolsController != null)
            toolsController.OnToolChanged -= RefreshDraggable;

        StopAllCoroutines();
    }

    public void RefreshDraggable(Tool tool)
    {
        draggable = tool != null? tool.TypeTool == ToolType.Recycler : false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!draggable)
        {
            OnEndDrag(eventData);
            return;
        }

        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(eventData.position) + dragPointOffset;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!draggable)
            return;

        dragPointOffset = transform.position - Camera.main.ScreenToWorldPoint(eventData.position);

        if (rb != null)
        {
            rb.simulated = false;
        }

        GetComponentInChildren<SpriteRenderer>().sortingLayerName = "UI";
        GetComponentInChildren<SpriteRenderer>().sortingOrder = 1000;


        //GetComponent<HingeJoint2D>().enabled = true;
        //GetComponent<HingeJoint2D>().anchor = Camera.main.ScreenToWorldPoint(eventData.position)- transform.position ;
        // GetComponent<HingeJoint2D>().connectedBody = FindObjectOfType<CursorJoint>().GetComponent<Rigidbody2D>();


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!draggable)
            return;

        if (rb != null)
        {
            rb.simulated = true;
            rb.velocity = Vector3.zero;
            rb.AddForceAtPosition(eventData.delta, Camera.main.ScreenToWorldPoint(eventData.position), ForceMode2D.Impulse);
        }

        GetComponentInChildren<SpriteRenderer>().sortingLayerName = "Interactable Objects";
        GetComponentInChildren<SpriteRenderer>().sortingOrder = 20;

        //GetComponent<HingeJoint2D>().connectedBody = null;
        //GetComponent<HingeJoint2D>().enabled = false;
        //FindObjectOfType<RecyclerController>().CloseRecycler();
    }

    private IEnumerator CloseRecyclerNextFrame()
    {
        yield return null;

        FindObjectOfType<RecyclerController>().CloseRecycler();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StartCoroutine(CloseRecyclerNextFrame());
    }



    #endregion

}
