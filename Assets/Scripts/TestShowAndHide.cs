using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShowAndHide : MonoBehaviour
{
    private SpriteRenderer testSprite;

    private void Start()
    {
        testSprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        testSprite.enabled = (InputManagement.buttons["LeftMouseButton"].isDown);
    }
}
