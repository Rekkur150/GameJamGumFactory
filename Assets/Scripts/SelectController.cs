using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectController : MonoBehaviour
{
    public static SelectController instance = null;

    public GameObject selectedObject;

    public TMP_Text Name;
    public TMP_Text Description;
    public TMP_Text Information;

    public Button Purchase;
    public Button Upgrade;
    public Button Downgrade;
    public Button DeleteButton;

    public MenuToggle InformationPanel;


    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

        Upgrade.onClick.AddListener(UpgradeBuilding);
        Downgrade.onClick.AddListener(DowngradeBuilding);
        DeleteButton.onClick.AddListener(DeleteBuilding);
        Purchase.onClick.AddListener(PurchaseBuilding);

    }

    void PurchaseBuilding()
    {
        Building build = selectedObject.GetComponent<Building>();

        if (build != null)
        {

            if (MoneyController.RemoveFunds(build.Cost))
            {
                if (build.toDelete != null)
                {
                    Destroy(build.toDelete);
                }

                if (build.toSetActive != null)
                {
                    build.toSetActive.SetActive(true);
                }
            } else
            {
                return;
            }
        }

        CloseInformation();

    }

    void DeleteBuilding()
    {

        if (selectedObject != null)
        {
            BuildingsScript.buildingsScript.SellBuilding(selectedObject);
            selectedObject = null;
            TileController.ResetSelectTile();
        }
    }

    void UpgradeBuilding()
    {

        Building build = selectedObject.GetComponent<Building>();
        MachineController controller = selectedObject.GetComponent<MachineController>();
        EnvironmentController envir = selectedObject.GetComponent<EnvironmentController>();


        if (build != null)
        {
            if (build.UpgradeLevel < build.UpgradeLimit && MoneyController.RemoveFunds(build.NextUpgradeCost))
            {

                build.Upgrade();


                if (controller != null)
                {
                    for (int i = 0; i < build.Fields.Length; i++)
                    {

                        float multiplier = 0;
                        if (build.Fields[i] == "dispenseRate")
                        {
                            multiplier = 1f / build.UpgradeMultiplier;
                        }
                        else
                        {
                            multiplier = build.UpgradeMultiplier;
                        }

                        typeof(MachineController).GetField(build.Fields[i]).SetValue(controller, float.Parse(typeof(MachineController).GetField(build.Fields[i]).GetValue(controller).ToString()) * multiplier);
                    }

                    controller.Updated();
                }

                if (envir != null)
                {
                    for (int i = 0; i < build.Fields.Length; i++)
                    {
                        typeof(EnvironmentController).GetField(build.Fields[i]).SetValue(envir, float.Parse(typeof(EnvironmentController).GetField(build.Fields[i]).GetValue(envir).ToString()) * build.UpgradeMultiplier);
                    }
                    envir.Upgraded();
                }

               

                SetBuildingInformation(selectedObject);
            }
        }
    }

    void DowngradeBuilding()
    {
        Building build = selectedObject.GetComponent<Building>();
        MachineController controller = selectedObject.GetComponent<MachineController>();
        EnvironmentController envir = selectedObject.GetComponent<EnvironmentController>();


        if (build != null)
        {
            if (build.UpgradeLevel > 1)
            {
                MoneyController.AddFunds(build.PreviousUpgradeCost);
                build.Downgrade();


                if (controller != null)
                {
                    for (int i = 0; i < build.Fields.Length; i++)
                    {

                        float multiplier = 0;
                        if (build.Fields[i] == "dispenseRate")
                        {
                            multiplier = build.UpgradeMultiplier;
                        }
                        else
                        {
                            multiplier = 1f / build.UpgradeMultiplier;
                        }

                        typeof(MachineController).GetField(build.Fields[i]).SetValue(controller, float.Parse(typeof(MachineController).GetField(build.Fields[i]).GetValue(controller).ToString()) * multiplier);
                    }

                    controller.Updated();
                }

                if (envir != null)
                {
                    for (int i = 0; i < build.Fields.Length; i++)
                    {
                        typeof(EnvironmentController).GetField(build.Fields[i]).SetValue(envir, float.Parse(typeof(EnvironmentController).GetField(build.Fields[i]).GetValue(envir).ToString()) / build.UpgradeMultiplier);
                    }
                    envir.Upgraded();
                }



                SetBuildingInformation(selectedObject);
            }
        }
    }

    public void CloseInformation()
    {
        InformationPanel.RemoveCanvasActive();
    }

    string FloatToString(float money)
    {

        string text = "";

        if (money < 1000)
        {
            text += money.ToString();
        }
        else if (money < 1000000f)
        {
            text += (money / (1000f)).ToString() + "K";
        }
        else if (money < 1000000000f)
        {
            text += (money / (1000000f)).ToString() + "M";
        }
        else if (money < 1000000000000f)
        {
            text += (money / (1000000000f)).ToString() + "B";
        }
        else
        {
            text += (money / (1000000000000f)).ToString() + "T";
        }

        return text;
    }

    public void SetBuildingInformation(GameObject obj)
    {

        InformationPanel.SetCanvasActive();

        selectedObject = obj;

        Building build = obj.GetComponent<Building>();
        MachineController controller = obj.GetComponent<MachineController>();
        EnvironmentController envir = obj.GetComponent<EnvironmentController>();

        if (build != null)
        {

            if (Purchase != null)
            {
                Purchase.transform.gameObject.SetActive(build.CanBePurchased);
                Purchase.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = string.Format("Purchase ({0:#0.00})", FloatToString(Mathf.Round(build.Cost)));
            }
            Upgrade.transform.gameObject.SetActive(build.CanBeUpgraded && build.UpgradeLevel < build.UpgradeLimit);
            Upgrade.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = string.Format("Upgrade ({0:#0.00})", FloatToString(Mathf.Round(build.NextUpgradeCost)));
            Downgrade.transform.gameObject.SetActive(build.CanBeDowngraded && build.UpgradeLevel > 1);
            Downgrade.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = string.Format("Downgrade ({0:#0.00})", FloatToString(Mathf.Round(build.PreviousUpgradeCost)));
            DeleteButton.transform.gameObject.SetActive(build.CanBeDeleted);
            DeleteButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = string.Format("Delete ({0:#0.00})", FloatToString(Mathf.Round(build.NetCostOfBuilding)));

            Name.text = build.Name;
            Description.text = build.Description;

            Information.text = "";
            Information.text += string.Format("Cost: {0:#0.00} \n", Mathf.Round(build.Cost));
            Information.text += string.Format("Level: {0:#0.00} \n", build.UpgradeLevel);

            if (controller != null)
            {
                for (int i = 0; i < build.Fields.Length; i++)
                {
                    Information.text += string.Format(build.Fields[i] + ": {0:#0.00} \n", FloatToString(float.Parse(typeof(MachineController).GetField(build.Fields[i]).GetValue(controller).ToString())));
                }
            }

            if (envir != null)
            {
                for (int i = 0; i < build.Fields.Length; i++)
                {
                    Information.text += string.Format(build.Fields[i] + ": {0:#0.00} \n", FloatToString(float.Parse(typeof(EnvironmentController).GetField(build.Fields[i]).GetValue(envir).ToString())));
                }
            }
        }
    }
}
