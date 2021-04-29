using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    public static ObjectController instance;


    [Tooltip("The Object that will be cloned")]
    public GameObject masterObject;

    [Tooltip("The max number of objects")]
    public int numberOfObjects = 100;

    [Tooltip("This collider will be the range of which the objects will be dispersed")]
    public Collider coll;

    [Tooltip("The hierarchy location objects will be cloned to, default is this GameObject")]
    public GameObject cloneRepository = null;

    [Tooltip("Respawn object after object picked up")]
    public bool respawnAfterPickup = false;

    [Tooltip("Generate Objects before hand, to limit creating and removing objects, this allows for objects not generated to be incorperated if false")]
    public bool generateAllObjectsBeforeHand = false;

    //List of objects
    private List<GameObject> objectList = new List<GameObject>();

    private int ActiveObjects = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }


        if (cloneRepository == null)
        {
            cloneRepository = gameObject;
        }

        if (generateAllObjectsBeforeHand)
        {
            GenerateObjects();
            DistributeObjects();
        }
    }

    public GameObject GetClosestObject(Transform trans)
    {
        GameObject closest = objectList[0];

        if (closest == null)
        {
            return null;
        }

        float closestDistance = Vector3.Distance(objectList[0].transform.position, trans.position);
        for (int i = 1; i < objectList.Count; i++)
        {

            float tempDistance = Vector3.Distance(objectList[i].transform.position, trans.position);
            if (tempDistance < closestDistance)
            {
                closestDistance = tempDistance;
                closest = objectList[i];
            }
        }

        return closest;
    }

    public void CollectedObject(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        obj.SetActive(false);
        ActiveObjects--;

        if (respawnAfterPickup)
        {
            RespawnObject();
        }

        if (!generateAllObjectsBeforeHand)
        {
            objectList.Remove(obj);
            Destroy(obj);
        }
    }

    public void MoveRandomObjectToLocation(Vector3 pos)
    {
        objectList[0].transform.position = pos;
    }

    public void AddObjectToList(GameObject obj)
    {
        objectList.Add(obj);
    }

    private void RespawnObject()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (!objectList[i].activeInHierarchy)
            {
                DistributeObject(objectList[i]);
            }
        }
    }

    private void GenerateObjects()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            objectList.Add(Instantiate(masterObject, cloneRepository.transform));
        }
    }

    public void DistributeObjects()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            DistributeObject(objectList[i]);
        }
    }

    private void DistributeObject(GameObject obj)
    {

        obj.transform.position = new Vector3(
            Random.Range(coll.bounds.min.x, coll.bounds.max.x),
            Random.Range(coll.bounds.min.y, coll.bounds.max.y),
            Random.Range(coll.bounds.min.z, coll.bounds.max.z));
        obj.SetActive(true);
        ActiveObjects++;
    }

}

