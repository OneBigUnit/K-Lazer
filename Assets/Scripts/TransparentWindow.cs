using UnityEngine;
using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;

public class TransparentWindow : MonoBehaviour
{
    const int GWL_EXSTYLE = -20;
    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;

    static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

    private IntPtr hWnd;

     private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    [DllImport("user32.dll")]
    private static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("user32.dll")]
    private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    public void SetClickThrough(bool clickthrough)
    {
        if (clickthrough && !Laser.allowedFlag) {
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
            Debug.Log("Clickthrough!");
        } else
        {
            SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED);
            Debug.Log("Not Clickthrough!");
        }
    }

    public void SetTopmost()
    {
        SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0, 0);
    }

    private void Start()
    {
#if !UNITY_EDITOR
        Application.runInBackground = true;

        hWnd = GetActiveWindow();

        MARGINS margins = new MARGINS { cxLeftWidth = -1 };
        DwmExtendFrameIntoClientArea(hWnd, ref margins);

        SetWindowLong(hWnd, GWL_EXSTYLE, WS_EX_LAYERED | WS_EX_TRANSPARENT);
        SetTopmost();
#endif
    }

    public bool MouseIsIntersecting()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current) { pointerId = -1, };

        // Canvas Raycast
        pointerData.position = Input.mousePosition;
        List<RaycastResult> canvasOverlaps = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, canvasOverlaps);
        List<RaycastResult> filteredCanvasOverlaps = canvasOverlaps.Where(result => (result.gameObject.GetComponent<Collider2D>() != null) || (result.gameObject.GetComponent<Collider>() != null)).ToList();
        bool canvasResults = filteredCanvasOverlaps.Count > 0;

        // World Raycast
        Vector2 mouseWorldLocation = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        bool worldResults = Physics2D.OverlapPointAll(mouseWorldLocation).Length > 0;

        return canvasResults || worldResults;
    }

    private void Update()
    {
        SetClickThrough(!MouseIsIntersecting());

        Application.runInBackground = true;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
