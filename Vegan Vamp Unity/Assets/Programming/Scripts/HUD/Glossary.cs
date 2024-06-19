using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Glossary: MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //compoonents
    [SerializeField] GameObject[] ingredients;


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

    public void AddEntry(string ingredient)
    {
        for (int i = 0; i < ingredients.Length; i++)
        {
            //get ingredient
            if (ingredients[i].name == ingredient)
            {
                //go through each child of the object
                foreach (Transform child in ingredients[i].transform)
                {
                    //find hidden and deactivate
                    child.transform.Find("Hidden").gameObject.SetActive(false);
                }

                break;
            }
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region



    #endregion
    //========================


}
