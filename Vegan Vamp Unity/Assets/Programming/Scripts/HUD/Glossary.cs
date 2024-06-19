using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Glossary: MonoBehaviour
{
    //IMPORTS
    //========================
    #region



    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    Image[] images;
    TextMeshProUGUI[] names;
    TextMeshProUGUI[] descriptions;

    Dictionary<string, int> ingredientKnowledge = new Dictionary<string, int>()
    {
        {"Health Ingredient", 0},
        {"Fire Ingredient", 0},
        {"Ice Ingredient", 0},
        {"Tornado Ingredient", 0},
        {"Speed Ingredient", 0},
        {"Gravity Ingredient", 0},
        {"Teleport Ingredient", 0}

    };

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void AddKnowledge(string ingredient)
    {

    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region



    #endregion
    //========================


}
