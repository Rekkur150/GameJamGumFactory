using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public string Name;
    public int Id;
    public float Cost;
    public float BaseUpgradeCost;
    public float NextUpgradeCost;
    public float PreviousUpgradeCost;
    public float NetCostOfBuilding = 0f;
    public float UpgradeCostMultiplier = 2;
    public float UpgradeMultiplier = 2;
    public int UpgradeLevel = 1;
    public int UpgradeLimit = 25;
    public GameObject par;

    public string Tooltip;

    public string Description;
    public string[] Fields;

    public bool CanBeUpgraded = false;
    public bool CanBeDeleted = true;
    public bool CanBeDowngraded = false;
    public bool CanBeRotated = true;
    public bool CanBePurchased = false;

    public GameObject toDelete;
    public GameObject toSetActive;

    void Start()
    {
        par = gameObject;
        NextUpgradeCost = BaseUpgradeCost;
        NetCostOfBuilding = Cost;
    }

    public void Upgrade()
    {
        PreviousUpgradeCost = NextUpgradeCost;
        NetCostOfBuilding += NextUpgradeCost;
        NextUpgradeCost = NextUpgradeCost * UpgradeCostMultiplier;
        UpgradeLevel++;
    }

    public void Downgrade()
    {
        NextUpgradeCost = PreviousUpgradeCost;
        PreviousUpgradeCost = PreviousUpgradeCost / UpgradeCostMultiplier;
        NetCostOfBuilding -= NextUpgradeCost;
        UpgradeLevel--;
    }
}
