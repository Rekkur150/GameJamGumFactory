using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContoller : MonoBehaviour
{

    public float speed = 1f;
    public static bool canEdit = true;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey("w"))
        {
            rb.AddForce(transform.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey("s"))
        {
            rb.AddForce(-transform.forward * speed * Time.deltaTime);
        }

        if (Input.GetKey("a"))
        {
            rb.AddForce(-transform.right * speed * Time.deltaTime);
        }

        if (Input.GetKey("d"))
        {
            rb.AddForce(transform.right * speed * Time.deltaTime);
        }

        if (Input.GetKey("space"))
        {
            rb.AddForce(transform.up * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.AddForce(-transform.up * speed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Cancel"))
        {
            BuildingsScript.buildingsScript.ChangeBuildingType(0);
            TileController.ResetSelectTile();
        }

        if (Input.GetButtonDown("Rotate"))
        {
            if (TileController.selectedTile)
            {
                if (TileController.selectedTile.transform.childCount > 0 && !BuildingsScript.buildingsScript.PhantomInHand())
                {
                    Transform temp = TileController.selectedTile.transform.GetChild(0);
                    Building build = temp.GetComponent<Building>();
                    if (build)
                    {
                        if (build.CanBeRotated)
                        {
                            temp.Rotate(0f, 90f, 0f, Space.Self);
                        }
                    }
                } else
                {
                    BuildingsScript.buildingsScript.RotatePhantomBuilding();
                }
            }

        } 

        if (Input.GetButtonDown("Delete"))
        {
            if (TileController.selectedTileForInformation && TileController.selectedTile == TileController.selectedTileForInformation && TileController.selectedTileForInformation.transform.childCount > 0)
            {
                Transform temp = TileController.selectedTileForInformation.transform.GetChild(0);
                BuildingsScript.buildingsScript.SellBuilding(temp.gameObject);
                TileController.ResetSelectTile();
            }
            else if (TileController.selectedTile && TileController.selectedTile.transform.childCount > 0)
            {
                Transform temp = TileController.selectedTile.transform.GetChild(0);
                BuildingsScript.buildingsScript.SellBuilding(temp.gameObject);
            }
        }

        if (Input.GetButtonDown("Select"))
        {
            if (BuildingsScript.buildingsScript.GetBuildingType() == 0)
            {
                TileController.SetSelectTile();
            }
        }

        if (Input.GetButtonDown("Menu"))
        {
            MenuToggle temp = GetComponent<MenuToggle>();
            if (temp)
            {
                temp.ToggleCanvas();
            }
        }

    }
}
