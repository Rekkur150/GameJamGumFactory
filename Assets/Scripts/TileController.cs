using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public static Material transparent;
    public static Material selected;
    public static Material highlighted;

    private static float duration = 2f;

    private Renderer rend;

    public static bool canSelect = true;
    public static GameObject selectedTile;
    public static GameObject selectedTileForInformation;


    void Start()
    {
        rend = GetComponent<Renderer>();

        rend.material = transparent;
    }

    public void ResetMaterial()
    {
        rend.material = transparent;
    }

    void OnMouseOver()
    {
        if (canSelect && gameObject != selectedTileForInformation)
        {
            selectedTile = gameObject;
            rend.material.Lerp(transparent, highlighted, duration);
        } 
        else if (canSelect && gameObject == selectedTileForInformation)
        {
            selectedTile = gameObject;
        }
    }

    void OnMouseExit()
    {
        if (canSelect && gameObject != selectedTileForInformation)
        {
            selectedTile = null;
            rend.material.Lerp(highlighted, transparent, duration);
        }
        else if (canSelect && gameObject == selectedTileForInformation)
        {
            selectedTile = null;
        }
    }

    public static void SetSelectTile()
    {
        if (selectedTile != null)
        {
            if (selectedTileForInformation != selectedTile && selectedTile.transform.childCount > 0)
            {
                if (selectedTileForInformation != null)
                {
                    selectedTileForInformation.GetComponent<Renderer>().material.Lerp(selected, transparent, duration);
                }

                selectedTileForInformation = selectedTile;
                selectedTileForInformation.GetComponent<Renderer>().material.Lerp(highlighted, selected, duration);

                SelectController.instance.SetBuildingInformation(selectedTileForInformation.transform.GetChild(0).gameObject);
            }
            else
            {
                ResetSelectTile();
            }
        } else
        {

            if (canSelect)
            {
                ResetSelectTile();
            }
        }
    }

    public static void ResetSelectTile()
    {
        if (selectedTileForInformation != null)
        {

            if (selectedTileForInformation == selectedTile)
            {
                selectedTileForInformation.GetComponent<Renderer>().material.Lerp(selected, highlighted, duration);
            }
            else
            {
                selectedTileForInformation.GetComponent<Renderer>().material.Lerp(selected, transparent, duration);
            }

            SelectController.instance.CloseInformation();

            selectedTileForInformation = null;
        }
    }
}
