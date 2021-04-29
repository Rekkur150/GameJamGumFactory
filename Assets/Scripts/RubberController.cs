using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubberController : MonoBehaviour
{
    private ConstantForce force;
    public int timesScaled = 0;

    // Start is called before the first frame update
    void Start()
    {
        force = GetComponent<ConstantForce>();
    }

    void Update()
    {
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        OnCollisionStay(collision);
    }

    void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.tag == "Belt" || collision.gameObject.tag == "Ramp")
        {
            BeltController belt = collision.gameObject.GetComponent<BeltController>();

            if (belt)
            {
                force.force = belt.forceDirection * belt.GetSpeed();
            }
        }

        if (collision.gameObject.tag == "InputHopper")
        {
            InputHopper inputhopper = collision.gameObject.GetComponent<InputHopper>();
            force.force = Vector3.zero;
            if (inputhopper.AddHolding(gameObject))
            {
                Destroy(gameObject);
            }
        }
        if (collision.gameObject.tag == "RemoveForce")
        {
            force.force = Vector3.zero;
            ObjectController.instance.AddObjectToList(gameObject);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ramp")
        {
            force.force = Vector3.zero;
        }
    }

    void OnEnable()
    {
        if (force != null)
        {
            force.force = Vector3.zero;
        }  
    }
}
