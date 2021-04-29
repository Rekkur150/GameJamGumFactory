using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ButtonSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
   
    public int BuildingType = 0;

    public GameObject toolTipBox;
    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeSelector);
    }

    // Update is called once per frame
    void ChangeSelector()
    {
        BuildingsScript.buildingsScript.ChangeBuildingType(BuildingType);
    }

    public void UpdateToolTip(string text)
    {
        if (text != null)
        {
            toolTipBox.GetComponent<TMP_Text>().text = text;
        }
    } 

    public void OnPointerEnter(PointerEventData eventData)
    {
        toolTipBox.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        toolTipBox.SetActive(false);
    }
}
