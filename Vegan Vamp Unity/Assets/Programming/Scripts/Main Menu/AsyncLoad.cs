using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class AsyncLoad : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //game object
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject mainMenu;

    //component
    [SerializeField] TextMeshProUGUI loadText;

    AsyncOperation loadOperation;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    bool loading = false;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void StartLoad()
    {
        //deactivate menu and activate loading screen
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        loading = true;

        //start async load
        StartCoroutine(LoadLevelAsync("NewOpenWorld"));
    }

    IEnumerator LoadLevelAsync(string levelToLoad)
    {
        //start loading scene, but wait permission to load fully
        loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        loadOperation.allowSceneActivation = false;

        yield return null;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Update()
    {
        if (loading)
        {
            //update load text
            loadText.text = loadOperation.progress < 0.9f ? $"Carregando - {loadOperation.progress * 100}%" : "Carregando - 100%\nAperte ESPAÇO para começar";

            //Load if scene loaded and space pressed
            if (Input.GetKeyDown(KeyCode.Space) && loadOperation.progress >= 0.9f)
            {
                loadOperation.allowSceneActivation = true;
            }
        }
    }

    #endregion
    //========================
}
