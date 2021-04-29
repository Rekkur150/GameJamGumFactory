using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{

    public float BeltSpeed;
    public float RubberBaseCost = 10;

    // Start is called before the first frame update
    void Start()
    {
        BeltController.speed = BeltSpeed;
    }

    public void Upgraded()
    {

        if (BeltSpeed != 0)
        {
            BeltController.speed = BeltSpeed;
        }
        
        if (RubberBaseCost != 0)
        {
            InputHopper.baseCost = RubberBaseCost;
        }
    }
}
