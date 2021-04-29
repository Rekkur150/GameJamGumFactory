using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineController : MonoBehaviour
{
    public bool Running = true;
    public float dispenseRate = 0.5f;
    private float dispenseCurrent = 0f;

    public bool InfiniteDispencing = false;

    public float maxContents = 200;
    public int CurrentContents = 0;
    private List<GameObject> contents = new List<GameObject>();

    public int nextDispensed = 0;
    public ItemSpawner[] dispensers;

    public Color ColorToAdd;
    public float SizeMultiplier = 1f;
    public int ConsumerPerOneOutput = 1;
    public GameObject ToChangeColor;

    public bool MapPlaced = false;
    public GameObject ToDispense;


    // Start is called before the first frame update
    void Start()
    {
        dispenseCurrent = dispenseRate;
        if (ToChangeColor)
        {
            foreach (Transform child in ToChangeColor.transform)
            {
                child.gameObject.GetComponent<Renderer>().material.color = ColorToAdd;
            }
        }

    }

    public void Updated()
    {
        dispenseCurrent = 0;
        maxContents = Mathf.Round(maxContents);
    }

    public void AddObject(GameObject obj)
    {
        if (Running)
        {
            if (CurrentContents < maxContents)
            {
                obj.SetActive(false);
                contents.Add(obj);
                CurrentContents++;
            }
        }

    }

    public void DispenseObject()
    {
        if (MapPlaced)
        {
           

            if (InfiniteDispencing)
            {

                dispensers[nextDispensed].SpawnItem(Instantiate(ToDispense, dispensers[nextDispensed].transform.position, new Quaternion(0,0,0,1)));
                nextDispensed++;

                if (nextDispensed >= dispensers.Length)
                {
                    nextDispensed = 0;
                }
            } else if (CurrentContents - ConsumerPerOneOutput > 0)
            {
                dispensers[nextDispensed].SpawnItem(Instantiate(ToDispense, dispensers[nextDispensed].transform.position, new Quaternion(0, 0, 0, 1)));
                nextDispensed++;

                if (nextDispensed >= dispensers.Length)
                {
                    nextDispensed = 0;
                }

                CurrentContents = CurrentContents - ConsumerPerOneOutput;
            }

        } 
        else
        {
            if (CurrentContents - ConsumerPerOneOutput >= 0)
            {
                GameObject temp = contents[CurrentContents - 1];
                Color tempColor = temp.GetComponent<Renderer>().material.color;
                float redComp = 0f;
                float greenComp = 0f;
                float blueComp = 0f;

                temp.transform.localScale = temp.transform.localScale * SizeMultiplier;

                if (SizeMultiplier > 1f)
                {
                    temp.GetComponent<RubberController>().timesScaled++;
                }

                dispensers[nextDispensed].SpawnItem(temp);
                nextDispensed++;

                Color tAdded = new Color(ColorToAdd.r, ColorToAdd.g, ColorToAdd.b);

                for (int i = CurrentContents - ConsumerPerOneOutput; i < CurrentContents - 1; i++)
                {

                    float amountToMultiply = contents[i].GetComponent<RubberController>().timesScaled;

                    for (int x = 0; x < amountToMultiply; x++)
                    {
                        temp.transform.localScale = temp.transform.localScale * SizeMultiplier;
                    }
                    
                    tempColor = contents[i].GetComponent<Renderer>().material.color;

                    redComp = tempColor.r + tAdded.r;
                    greenComp = tempColor.g + tAdded.g;
                    blueComp = tempColor.b + tAdded.b;

                    if (greenComp > 1)
                    {
                        greenComp = 1f;
                    }
                    if (redComp > 1)
                    {
                        redComp = 1f;
                    }
                    if (blueComp > 1)
                    {
                        blueComp = 1f;
                    }

                    tAdded = new Vector4(redComp, greenComp, blueComp, tempColor.a);

                    Destroy(contents[i]);
                }

                tempColor = temp.GetComponent<Renderer>().material.color;

                redComp = tAdded.r + tempColor.r;
                greenComp = tAdded.g + tempColor.g;
                blueComp = tAdded.b + tempColor.b;

                if (greenComp > 1)
                {
                    greenComp = 1f;
                }
                if (redComp > 1)
                {
                    redComp = 1f;
                }
                if (blueComp > 1)
                {
                    blueComp = 1f;
                }

                temp.GetComponent<Renderer>().material.color = new Vector4(redComp, greenComp, blueComp, tempColor.a);


                contents.RemoveRange(CurrentContents - ConsumerPerOneOutput, ConsumerPerOneOutput);
                CurrentContents = CurrentContents - ConsumerPerOneOutput;

                if (nextDispensed >= dispensers.Length)
                {
                    nextDispensed = 0;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Running)
        {
            if (dispenseCurrent < 0)
            {
                DispenseObject();
                dispenseCurrent = dispenseRate;
            }
            else
            {
                dispenseCurrent = dispenseCurrent - Time.deltaTime;
            }
        } else
        {
            
        }
    }
}
