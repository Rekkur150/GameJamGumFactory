using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileControllerController : MonoBehaviour
{
    public Material transparent;
    public Material selected;
    public Material highlighted;
    public bool canSelect = true;

    // Start is called before the first frame update
    void Start()
    {

        TileController.canSelect = canSelect;

        TileController.transparent = transparent;
        TileController.selected = selected;
        TileController.highlighted = highlighted;
    }

}
