using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltController : MonoBehaviour
{

    public Vector3 forceDirection;
    public static float speed = 0.8f;
    public float speedOfBelt = 5f;
    public bool isRampBelt = false;

    // Start is called before the first frame update
    void Update()
    {
        forceDirection = -transform.forward;
    }

    public float GetSpeed()
    {
        if (isRampBelt)
        {
            if (speed < 3.8f)
            {
                return 3.8f;
            } else
            {
                return speed * speedOfBelt;
            }
            

        }
        return speed * speedOfBelt;
    }
}
