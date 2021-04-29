using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int gridWidth = 50;
    public int gridHeight = 50;

    public float tileWidth = 2;
    public float tileHeight = 2;

    public GameObject Tile;

    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position + new Vector3(tileWidth / 2, 0, tileHeight / 2);
        Tile.SetActive(true);

        for (int i = 0; i < gridWidth; i++)
        {
            for (int x = 0; x < gridHeight; x++)
            {
                Instantiate(Tile, startPosition + (transform.right * (i * Tile.transform.localScale.x)) + (transform.forward * (x * Tile.transform.localScale.z)), new Quaternion(0, 0, 0, 1), transform);
            }
        }

        Tile.SetActive(false);
    }

}
