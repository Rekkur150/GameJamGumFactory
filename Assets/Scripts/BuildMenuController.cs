using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuildMenuController : MonoBehaviour
{
    public static BuildMenuController instance = null;
    public GameObject Button;

    private int NumberOfMenus = 0;
    public float Spacing = 40f;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    public void CreateBuildMenuItem(Building build)
    {
        GameObject temp = Instantiate(Button, transform);
        temp.name = build.Name;
        temp.GetComponent<ButtonSelector>().BuildingType = build.Id;
        temp.GetComponent<ButtonSelector>().UpdateToolTip(build.Tooltip);
        temp.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = temp.name + " " + build.Cost.ToString();
        temp.transform.Translate(-Vector3.up * Spacing * NumberOfMenus);

        NumberOfMenus++;
    }
}
