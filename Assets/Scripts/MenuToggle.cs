using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuToggle : MonoBehaviour
{

    private Button button;

    private static GameObject CurrentCanvas = null;

    public GameObject CanvasToToggle;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(ToggleCanvas);
        }
        CanvasToToggle.SetActive(false);
    }

    public void ToggleCanvas()
    {
        if (CurrentCanvas != null && CurrentCanvas != CanvasToToggle)
        {
            CurrentCanvas.SetActive(false);
            CurrentCanvas = null;
        }

        if (CurrentCanvas == CanvasToToggle)
        {
            TileController.canSelect = true;
            BuildingsScript.buildingsScript.canPlace = true;
        }


        if (CanvasToToggle.activeInHierarchy)
        {
            CanvasToToggle.SetActive(false);
            CurrentCanvas = null;
        }
        else
        {
            CanvasToToggle.SetActive(true);
            CurrentCanvas = CanvasToToggle;
        }

        BuildingsScript.buildingsScript.ChangeBuildingType(0);
    }

    public void SetCanvasActive()
    {
        if (!CanvasToToggle.activeInHierarchy)
        {
            ToggleCanvas();
        }
    }

    public void RemoveCanvasActive()
    {
        if (CanvasToToggle.activeInHierarchy)
        {
            ToggleCanvas();
        }
    }

}
