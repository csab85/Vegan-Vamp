using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //IMPORTS
    //========================
    #region
    
    [SerializeField] public GameObject[] inventoryList;
    [SerializeField] GameObject inventoryUI;

    TextMeshProUGUI inventoryText;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    public Dictionary<string, int> ingredientsDict = new Dictionary<string, int>();

    string ingredientsText;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void AddItem(GameObject AddedItem)
    {
        if (AddedItem.layer == LayerMask.NameToLayer("Ingredient"))
        {
            ingredientsText = "";

            foreach (GameObject item in inventoryList)
            {
                if (item.name == AddedItem.name)
                {
                    ingredientsDict[item.name] += 1;
                }

                //update dict text
                ingredientsText += $"{item.name}: {ingredientsDict[item.name]}\n";
            }

            inventoryText.text = ingredientsText;
        }
    }

    public void DropItem(GameObject item)
    {
        Vector3 dropDistance = 1 * Vector3.forward;

        Instantiate(item, transform.position + dropDistance, Quaternion.Euler(Vector3.zero));
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        inventoryText = inventoryUI.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

        //fill dict
        foreach (GameObject item in inventoryList)
        {
            ingredientsDict[item.name] = 0;

            ingredientsText += $"{item.name}: {ingredientsDict[item.name]}\n";

            inventoryText.text = ingredientsText;
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (inventoryUI.activeSelf)
            {
                inventoryUI.SetActive(false);
            }

            else
            {
                inventoryUI.SetActive(true);
            }
        }

        if (Input.GetButtonDown("Drop"))
        {

        }
    }

    #endregion
    //========================


}
