using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header ("Imports")]
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] inventoryItemsArray;
    [SerializeField] GameObject[] worldItemsArray;

    GameObject bag;
    RectTransform bagRectTransform;
    GameObject spawnPoint;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Settings")]
    [SerializeField] Vector3 bigScale;
    [SerializeField] Vector3 bigPosit;
    [SerializeField] Vector3 smallScale;
    [SerializeField] Vector3 smallPosit;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void AddItem(GameObject AddedItem)
    {
        foreach (GameObject item in inventoryItemsArray)
        {
            print(AddedItem.name + " 2D");
            print(item.name);

            if ((AddedItem.name + " 2D") == item.name)
            {

                print("salv");
                Instantiate(item, spawnPoint.transform.position, Quaternion.identity);

                break;
            }
        }
    }

    public void DropItem(GameObject item)
    {
        Destroy(item);

        foreach (GameObject worldItem in worldItemsArray)
        {
            string worldName = worldItem.name.Remove(worldItem.name.Length - 4, 3);

            if (worldName == worldItem.name)
            {
                Vector3 spawnDistance = 2 * player.transform.forward;

                Instantiate(worldItem, player.transform.position + spawnDistance, Quaternion.identity);

                break;
            }
        }

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
        bag = transform.GetChild(0).gameObject;
        bagRectTransform = bag.GetComponent<RectTransform>();
        spawnPoint = bag.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            if (bagRectTransform.localScale.x > smallScale.x)
            {
                bagRectTransform.localScale = smallScale;
                bagRectTransform.anchoredPosition = smallPosit;
            }

            else
            {
                bagRectTransform.localScale = bigScale;
                bagRectTransform.anchoredPosition = bigPosit;
            }
        }

        if (Input.GetButtonDown("Drop"))
        {
            
        }
    }

    #endregion
    //========================


}
