using UnityEngine;

public class TestClicks : MonoBehaviour
{
    private SpriteRenderer squareSprite;

    private void Update()
    {
        squareSprite.enabled = Input.GetMouseButton(0);
    }

    private void Start()
    {
        squareSprite = GetComponent<SpriteRenderer>();
    }
}
