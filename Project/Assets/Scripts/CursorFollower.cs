using UnityEngine;

public class CursorFollower : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
