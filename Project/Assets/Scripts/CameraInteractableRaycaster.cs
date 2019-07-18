using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Camera))]
public class CameraInteractableRaycaster : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private float rayDistance = 200f;
    private Camera cam;

    #endregion

    #region Methods

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
            
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, mousePosition - transform.position, rayDistance, layerMask);

            foreach (var hit in hits)
            {
                var pointerDownHandler = hit.collider.GetComponent<IPointerDownHandler>();
                if (pointerDownHandler != null)
                {
                    pointerDownHandler.OnPointerDown(null);
                }
            }

        }
    }

    #endregion
}
