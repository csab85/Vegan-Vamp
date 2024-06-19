using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class AsyncLoad : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //game object
    [SerializeField] GameObject loadingScreen;
    [SerializeField] GameObject mainMenu;

    //scripts
    [SerializeField] LoadText loadText;

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

    public void LoadLevel(string levelToLoad)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);
        loading = true;
        
        StartCoroutine(LoadLevelAsync(levelToLoad));
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
            switch (loadText.deletedTexts)
            {
                case 0:

                    if (loadText.printedTexts == 0)
                    {
                        //wait a bit
                        if (!loadText.waiting && !loadText.waited)
                        {
                            StartCoroutine(loadText.PrintText(0, 1, true));
                        }

                        //print first text
                        if (!loadText.typing && !loadText.waiting)
                        {
                            StartCoroutine(loadText.PrintText(1, 0.1f));
                        }
                    }

                    if (loadText.printedTexts == 1)
                    {
                        //wait a bit
                        if (!loadText.waiting && !loadText.waited)
                        {
                            StartCoroutine(loadText.PrintText(0, 1, true));
                        }

                        //print second text
                        if (!loadText.typing && !loadText.waiting)
                        {
                            StartCoroutine(loadText.PrintText(2, 0.1f));
                        }
                    }

                    if (loadText.printedTexts == 2)
                    {
                        //wait a bit
                        if (!loadText.waiting && !loadText.waited)
                        {
                            StartCoroutine(loadText.PrintText(0, 1, true));
                        }

                        //print third text
                        if (!loadText.typing && !loadText.waiting)
                        {
                            StartCoroutine(loadText.PrintText(3, 0.1f));
                        }
                    }

                    //delete third text
                    if (loadText.printedTexts == 3)
                    {
                        //give some time for player to read
                        if (!loadText.waiting && !loadText.waited)
                        {
                            StartCoroutine(loadText.PrintText(0, 3, true));
                        }

                        if (!loadText.typing && !loadText.waiting)
                        {
                            StartCoroutine(loadText.DeleteText(3, 0.1f));
                        }
                    }

                    break;

                case 1:

                    //delete second text
                    if (!loadText.typing)
                    {
                        StartCoroutine(loadText.DeleteText(2, 0.1f));
                    }

                    break;

                case 2:

                    //delete first text
                    if (!loadText.typing)
                    {
                        StartCoroutine(loadText.DeleteText(1, 0.1f));
                    }

                    break;

                case 3:

                    //wait then delete scene
                    if (!loadText.waiting && !loadText.waited)
                    {
                        StartCoroutine(loadText.PrintText(0, 1, true));
                    }

                    if (!loadText.waiting && loadText.waited)
                    {
                        loadOperation.allowSceneActivation = true;
                    }

                    break;
            }
        }
    }

    #endregion
    //========================


}
