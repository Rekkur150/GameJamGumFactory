using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{

    public GameObject SpawnObject;
    public float SpawnRate = 500f;

    public bool infinteSpawn = true;
    public int MaxHolding = 100;

    private int HoldingAmount = 0;
    private float RateCountDown = 0;

    public bool aPartOfMachine = false;

    // Start is called before the first frame update
    void Start()
    {
        RateCountDown = SpawnRate;

        if (SpawnRate < 0.3f)
        {
            SpawnRate = 0.3f;
        }
    }

    public int GetHoldingAmount ()
    {
        return HoldingAmount;
    }

    public bool AddHoldingAmount(int amount)
    {
        if (HoldingAmount + amount > MaxHolding)
        {
            return false;
        } else
        {
            HoldingAmount = HoldingAmount + amount;
            return true;
        }
    }

    public void SpawnItem(GameObject obj)
    {
        obj.transform.position = transform.position + (transform.right * Random.Range(-0.5f, 0.5f));
        obj.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!aPartOfMachine)
        {
            if (RateCountDown < 0f && (HoldingAmount > 0f || infinteSpawn))
            {
                Instantiate(SpawnObject, transform.position + (transform.right * Random.Range(-0.5f, 0.5f)), new Quaternion(0, 0, 0, 1));
                RateCountDown = SpawnRate;
            }
            else
            {
                RateCountDown = RateCountDown - Time.deltaTime;
            }
        } 

    }
}
