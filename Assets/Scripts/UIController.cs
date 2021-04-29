using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TileController.canSelect = false;
        if (TileController.selectedTile)
        {
            TileController.selectedTile.GetComponent<TileController>().ResetMaterial();
            TileController.selectedTile = null;
        }

        BuildingsScript.buildingsScript.canPlace = false;
        int temp = BuildingsScript.buildingsScript.GetBuildingType();
        BuildingsScript.buildingsScript.ChangeBuildingType(0);
        BuildingsScript.buildingsScript.ChangeBuildingType(temp);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TileController.canSelect = true;
        BuildingsScript.buildingsScript.canPlace = true;
    }
}
