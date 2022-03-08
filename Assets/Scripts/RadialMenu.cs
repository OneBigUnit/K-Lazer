using UnityEngine;
using UnityEngine.UI;


public class RadialMenu : MonoBehaviour
{
    public int selectedOptionIdx;

    [SerializeField] private Color normalColour;
    [SerializeField] private Color highlightedColour;
    [SerializeField] private Text[] menuOptions;
    [SerializeField] private GameObject highlightBlock;

    private Vector2 mouseInput;
    private GameObject menu;

    private void Start()
    {
        menu = gameObject;
    }

    void Update()
    {
        if (menu.activeInHierarchy)
        { 
            // Get mouse position relative to centre
            mouseInput.x = Input.mousePosition.x - Screen.width / 2f;
            mouseInput.y = Input.mousePosition.y - Screen.height / 2f;
            mouseInput.Normalize();

            if (mouseInput != Vector2.zero)
            {
                float degreesAngle = (((Mathf.Atan2(mouseInput.y, -mouseInput.x) / Mathf.PI) * 180) + 360 + 90) % 360;  // To radians ==> To Rotated Degrees ==> To 0-360

                // Check the current active section
                for (int idx = 0; idx < menuOptions.Length; idx++)
                {
                    if (degreesAngle > idx * (360 / 3) && degreesAngle < (idx + 1) * (360 / 3))
                    {
                        // The section that is selected
                        menuOptions[idx].color = highlightedColour;
                        selectedOptionIdx = idx;

                        highlightBlock.transform.rotation = Quaternion.Euler(0, 0, idx * -(360 / 3));
                    } else
                    {
                        // All sections that aren't selected
                        menuOptions[idx].color = normalColour;
                    }
                }
            }
        }

        // Call Functions
        if (Input.GetMouseButtonDown(0))
        {
            switch (selectedOptionIdx)
            {
                case 0:

                    break;

                case 1:
                    break;

                case 2:
                    break;
            }
        }
    }

    public void Show()
    {
        menu.SetActive(true);
    }

    public void Hide()
    {
        menu.SetActive(false);
    }
}
