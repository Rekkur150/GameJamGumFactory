using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndSceneScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneController.instance.GoToMenu();
    }
}
