using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu: MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //game objects
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject profile;
    [SerializeField] GameObject glossary;
    [SerializeField] GameObject options;
    [SerializeField] GameObject exit;

    List<GameObject> pages;

    //scripts
    [SerializeField] ThirdPersonCamera camScript;
    [SerializeField] Movement playerMovement;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region



    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void OpenProfile()
    {
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }

        profile.SetActive(true);
    }

    public void OpenGlossary()
    {
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }

        glossary.SetActive(true);
    }

    public void OpenOptions()
    {
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }

        options.SetActive(true);
    }

    public void OpenExit()
    {
        exit.SetActive(true);
    }

    public void CloseMenu()
    {
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
            buttons.SetActive(false);

            EventSystem.current.SetSelectedGameObject(buttons.transform.Find("Glossary").gameObject);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            camScript.explorationCamera.SetActive(true);
            camScript.combatCamera.SetActive(true);

            playerMovement.moveSpeed = playerMovement.baseSpeed;
        }
    }

    public void ExitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CancelExit()
    {
        exit.SetActive(false );
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    private void Start()
    {
        //make list
        pages = new List<GameObject>();
        pages.Add(profile);
        pages.Add(glossary);
        pages.Add(options);
        pages.Add(exit);

        //select glossary
        EventSystem.current.SetSelectedGameObject(buttons.transform.Find("Glossary").gameObject);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {
            for (int i = 0; i < pages.Count; i++)
            {
                if (pages[i].activeSelf)
                {
                    CloseMenu();
                    break;
                }

                //open if all pages closed
                if (i == pages.Count - 1)
                {
                    OpenGlossary();
                    buttons.SetActive(true);

                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;

                    camScript.explorationCamera.SetActive(false);
                    camScript.combatCamera.SetActive(false);

                    playerMovement.moveSpeed = 0;
                }
            }
        }
    }

    #endregion
    //========================


}
