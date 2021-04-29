using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{

    public static SceneController instance = null;
    public GameObject loadingCanvas;
    public TMP_Text loadingText;
    public int CurrentScene = 0;

    void Start()
    {

        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }


    public void ChangeScene(int scene)
    {
        CurrentScene = scene;
        loadingCanvas.SetActive(true);
        StartCoroutine(LoadScene(scene));
    }


    public void GoToMenu()
    {
        instance.ChangeScene(0);
    }

    public void GoToNextScene()
    {
        ChangeScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator LoadScene(int changeToScene)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(changeToScene);

        while (!asyncOperation.isDone)
        {
            loadingText.text = "Loading " + (asyncOperation.progress * 100) + "%";
            yield return null;
        }
    }

}
