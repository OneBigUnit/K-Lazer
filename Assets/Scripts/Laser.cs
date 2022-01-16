using UnityEngine;
using UnityEngine.EventSystems;

public class Laser : MonoBehaviour
{
    public static bool allowedFlag = false;

    private bool activeFlag = false;
    private SpriteRenderer laserSprite;

    public void ChangeAllowedFlag()
    {
        EventSystem.current.SetSelectedGameObject(null);
        allowedFlag = !allowedFlag;
    }

    private void Start()
    {
        laserSprite = GetComponent<SpriteRenderer>();
    }

     private void Update()
    {
        laserSprite.enabled = activeFlag && allowedFlag;
        Cursor.visible = !(activeFlag && allowedFlag);
        activeFlag = Input.GetMouseButton(0);
    }
}
