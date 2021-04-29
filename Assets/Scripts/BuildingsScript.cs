using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsScript : MonoBehaviour
{

    public static BuildingsScript buildingsScript = null;

    public bool canPlace = true;
    public Material PhantomMaterial;

    public List<Building> Buildings;


    private GameObject temp;
    private int BuildingType = 0;

    private int Rotation = 0;

    public bool NeedMoneyToPurchase = false;

    // Start is called before the first frame update
    void Start()
    {

        canPlace = true;

        if (buildingsScript == null)
        {
            buildingsScript = this;
        } else if (buildingsScript != this)
        {
            Destroy(gameObject);
        }

        if (BuildMenuController.instance != null)
        {
            for (int i = 0; i < Buildings.Count; i++)
            {
                BuildMenuController.instance.CreateBuildMenuItem(Buildings[i]);
            }
        }




    }

    public void ChangeBuildingType(int newBuilding)
    {
        if (temp)
        {
            Destroy(temp);
        }

        if (BuildingType == newBuilding)
        {
            BuildingType = 0;
        } else
        {
            BuildingType = newBuilding;
        }
    }

    public int GetBuildingType()
    {
        return BuildingType;
    }

    public bool PhantomInHand()
    {
        if (BuildingType != 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void PlaceBuilding()
    {
        if (BuildingType != 0)
        {
            float cost = GetCostFromInt(BuildingType);

            if (!NeedMoneyToPurchase || (NeedMoneyToPurchase && MoneyController.GetFunds() >= cost))
            {

                if (TileController.selectedTile)
                {
                    if (TileController.selectedTile.transform.childCount > 0)
                    {
                        if (!SellBuilding(TileController.selectedTile.transform.GetChild(0).gameObject))
                        {
                            return;
                        }
                    }

                    if (NeedMoneyToPurchase)
                    {
                        MoneyController.RemoveFunds(cost);
                    }

                    CreateBuilding(GetGameObjectFromInt(BuildingType));
                }
            }
        }
    }

    public bool SellBuilding(GameObject obj)
    {

        int objId = GetIntFromGameObject(obj);

        if (objId > 200 || !obj.GetComponent<Building>().CanBeDeleted)
        {
            return false;
        }

        if (NeedMoneyToPurchase)
        {
            float cost = obj.GetComponent<Building>().NetCostOfBuilding;
            MoneyController.AddFunds(cost);
            
        }

        Destroy(obj);

        return true;
    }

    private int GetIntFromGameObject(GameObject obj)
    {
        Building objBuild = obj.GetComponent<Building>();

        if (objBuild != null)
        {
            return objBuild.Id;
        }

        return 1;
    }

    private GameObject GetGameObjectFromInt(int number)
    {

        for (int i = 0; i < Buildings.Count; i++)
        {
            if (number == Buildings[i].Id)
            {
                return Buildings[i].par;
            }
        }

        return Buildings[0].par;
    }

    private float GetCostFromInt(int number)
    {
        for (int i = 0; i < Buildings.Count; i++)
        {
            if (number == Buildings[i].Id)
            {
                return Buildings[i].Cost;
            }
        }

        return Buildings[0].Cost;
    }

    public void RotatePhantomBuilding()
    {
        if (temp)
        {
            temp.transform.Rotate(0f, 90f, 0f, Space.Self);
            Rotation++;
            if (Rotation > 4)
            {
                Rotation = 0;
            }
        }
    }


    private void CreateBuilding(GameObject Object)
    {
        GameObject toBePlaced = Instantiate(Object, TileController.selectedTile.transform.position, new Quaternion(0, 0, 0, 1));
        toBePlaced.transform.SetParent(TileController.selectedTile.transform);
        toBePlaced.transform.position = TileController.selectedTile.transform.position;
        toBePlaced.transform.rotation = temp.transform.rotation;
    }

    private void ChangeToPhantom(Transform trans)
    {
        foreach (Transform child in trans)
        {
            child.gameObject.layer = 8;
            Renderer rend = child.gameObject.GetComponent<Renderer>();
            if (rend)
            {
                rend.material.color = rend.material.color + new Color(0, 0, 0.5f, 1);
            }
            ChangeToPhantom(child);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButton(0) && canPlace)
        {
            
            PlaceBuilding();
        }

        if (BuildingType != 0)
        {
            if (TileController.selectedTile)
            {
                if (temp)
                {
                    temp.transform.position = TileController.selectedTile.transform.position + new Vector3(0, 0.1f, 0);
                } else
                {
                    temp = Instantiate(GetGameObjectFromInt(BuildingType), TileController.selectedTile.transform.position + new Vector3(0, 0.1f, 0), new Quaternion(0, 0, 0, 1));
                    temp.transform.Rotate(0f, 90f * Rotation, 0f, Space.Self);
                    ChangeToPhantom(temp.transform);
                }
            }
        }
    }
}
