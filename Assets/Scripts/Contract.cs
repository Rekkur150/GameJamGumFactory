using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Contract : MonoBehaviour
{
    public float GumAmount = 0f;
    public float ContractFufilled = 10f;
    public float Red;
    public float Green;
    public float Blue;

    public float Size;

    public static float Spacing = 28f;

    public static int AmountOfContracts = -1;
    public static int ContractsCompleted = 0;
    public GameObject ContractTextBox;
    private TMP_Text Text;
    public bool ContractCompleted = false;


    void Start()
    {
        AmountOfContracts++;

        GameObject temp = Instantiate(ContractTextBox);
        Text = temp.transform.GetChild(0).GetComponent<TMP_Text>();
        Text.transform.Translate(-Vector3.up * Spacing * AmountOfContracts);
        UpdateTextBox();
    }

    void UpdateTextBox()
    {
        Text.text = "";

        if (Red != 0)
        {
            Text.text += "Red:" + 100 * (Red / (4f)) + "% ";
        }

        if (Green != 0)
        {
            Text.text += "Green:" + 100 * (Green / (4f)) + "% ";
        }

        if (Blue != 0)
        {
            Text.text += "Blue:" + 100 * (Blue / (4f)) + "% ";
        }

        if (Size != 0)
        {
            Text.text += "Size:" + Size;
        }

        Text.text += " (" + GumAmount + "/" + ContractFufilled + ")";
    }

    // Start is called before the first frame update
    public bool CheckObject(GameObject obj)
    {
        Color temp = obj.GetComponent<Renderer>().material.color;

        bool ChecksOut = true;

        if (Mathf.Round(temp.r/(0.25f)) != Red)
        {
            ChecksOut = false;
        }
        if (Mathf.Round(temp.g / (0.25f)) != Green)
        {
            ChecksOut = false;
        }
        if (Mathf.Round(temp.b / (0.25f)) != Blue)
        {
            ChecksOut = false;
        }
        if (Mathf.Round(temp.g / (0.25f)) != Green)
        {
            ChecksOut = false;
        }

        if (obj.GetComponent<RubberController>().timesScaled != Size)
        {
            ChecksOut = false;
        }

        if (ChecksOut && !(GumAmount >= ContractFufilled))
        {
            GumAmount++;
            UpdateTextBox();
        }

        if (GumAmount >= ContractFufilled)
        {
            if (!ContractCompleted)
            {
                ContractCompleted = true;
                ContractsCompleted++;
                CheckToSeeIfCompletedAllContracts();

            }

        }

        return ChecksOut;

    }


    void CheckToSeeIfCompletedAllContracts()
    {

        if (ContractsCompleted >= AmountOfContracts+1)
        {
            ContractController.instance.OpenCompletedMenu();
        }
    }

}
