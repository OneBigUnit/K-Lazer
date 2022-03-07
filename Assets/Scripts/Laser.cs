using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class Laser : MonoBehaviour
{
    public static bool allowedFlag = false;

    [SerializeField] private GameObject clickManager;
    [SerializeField] private GameObject transparentWindow;

    private bool activeFlag = false;
    private SpriteRenderer laserSprite;
    private ClickDetection clickDetectionScript;
    private TransparentWindow transparentWindowScript;

    public void ChangeAllowedFlag()
    {
        EventSystem.current.SetSelectedGameObject(null);
        allowedFlag = !allowedFlag;
    }

    private IEnumerator DoLogic()
    {
        CoroutineWithData coroutine = new CoroutineWithData(this, clickDetectionScript.DoubleClickedAndHolding());
        yield return coroutine.coroutine;
        Debug.Log(coroutine.result);
        activeFlag = (bool)coroutine.result;
    }

    private void Start()
    {
        laserSprite = GetComponent<SpriteRenderer>();
        clickDetectionScript = clickManager.GetComponent<ClickDetection>();
        transparentWindowScript = transparentWindow.GetComponent<TransparentWindow>();
    }

     private void Update()
    {
        laserSprite.enabled = activeFlag && allowedFlag;
        Cursor.visible = !(activeFlag && allowedFlag);
<<<<<<< HEAD
        activeFlag = InputManagement.buttons["LeftMouseButton"].IsDoubleClickedAndHeld(.3f);
=======
        StartCoroutine(DoLogic());
>>>>>>> b75ef5e392afd7ecd0e9ca4baae1a104bae58621
    }
}
