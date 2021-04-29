using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractController : MonoBehaviour
{
    public static ContractController instance = null;
    public GameObject completedCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Contract.AmountOfContracts = -1;
        Contract.ContractsCompleted = 0;

        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void OpenCompletedMenu()
    {
        completedCanvas.SetActive(true);
    }

}
