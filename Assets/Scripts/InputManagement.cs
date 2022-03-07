using System.Runtime.InteropServices;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;


public enum Buttons : int
{
    LeftMouseButton = 0x01
}


public class InputManagement : MonoBehaviour
{
    public static Dictionary<string, Button> buttons = new Dictionary<string, Button>
    {
        { "LeftMouseButton", new Button(0x01) }
    };


private void Update()
    {
        foreach (Button supportedButton in buttons.Values)
        {
            supportedButton.Update();
        }
    }
}


public class Button
{
    public int keyCode;
    public float lastPressTime;
    public float lastReleaseTime;
    public bool isDown;
    public bool isUp;
    public bool wasDown;
    public bool wasUp;
    public int clickCount = 0;

    public float multiClickThreshold = 0.3f;

    [DllImport("user32.dll")]
    private static extern short GetKeyState(int nVirtualKey);

    public Button(int keyCode)
    {
        this.keyCode = keyCode;
    }

    public void Update()
    {
        UpdateState();

        if (WasPressedThisFrame())
        {
            lastPressTime = Time.time;
        } else if (WasReleasedThisFrame())
        {
            lastReleaseTime = Time.time;
        }
    }

    public bool IsDoubleClicked()
    {
        return clickCount >= 2;
    }

    public bool IsHeld(float thresholdTimeHeld)
    {
        return (isDown) && (TimeSince(lastPressTime) >= thresholdTimeHeld);
    }

    public bool IsDoubleClickedAndHeld(float timeHeld)
    {
        if (IsDoubleClicked())
        {
            return IsHeld(timeHeld);
        }
        return false;
    }

    private void UpdateState()
    {
        wasDown = isDown;
        wasUp = isUp;

        isDown = IsDown();
        isUp = IsUp();

        UpdateClickCount();
    }

    public bool WasPressedThisFrame()
    {
        return wasUp && isDown;
    }
    
    private void UpdateClickCount()
    {
        if (WasPressedThisFrame() && clickCount == 0)
        {
            clickCount++;
        }
        else if (WasPressedThisFrame() && TimeSince(lastPressTime) < multiClickThreshold)
        {
            clickCount++;
        } else if (TimeSince(lastPressTime) > multiClickThreshold && isUp)
        {
            clickCount = 0;
        }
    }

    private float TimeSince(float eventTime)
    {
        return Time.time - eventTime;
    } 

    public bool WasReleasedThisFrame()
    {
        return wasDown && isUp;
    }

    private bool IsDown()
    {
        short keyState = GetKeyState(keyCode);
        return keyState == -128 || keyState == -127;
    }

    private bool IsUp()
    {
        short keyState = GetKeyState(keyCode);
        return keyState == 0 || keyState == 1;
    }
}
