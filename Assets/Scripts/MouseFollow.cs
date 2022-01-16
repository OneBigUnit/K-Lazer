using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private Transform laserPointerTransform;

    private void moveTo(float horizontalDistance, float verticalDistance)
    {
        laserPointerTransform.position = new Vector2(horizontalDistance, verticalDistance);
    }

    private void Start()
    {
        laserPointerTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        moveTo(mousePosition.x, mousePosition.y);
    }
}
