using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HolsteredGraper : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject grapesObj;
    [SerializeField] Gun graperScript;

    public List<GameObject> grapes = new List<GameObject>();

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



    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        //get game objects
        foreach (Transform grape in grapesObj.transform)
        {
            grapes.Add(grape.gameObject);
        }
    }

    void OnEnable()
    {
        for (int i = 0; i < grapes.Count; i++)
        {
            grapes[i].SetActive(graperScript.grapes[i].activeSelf);
        }
    }

    #endregion
    //========================


}
