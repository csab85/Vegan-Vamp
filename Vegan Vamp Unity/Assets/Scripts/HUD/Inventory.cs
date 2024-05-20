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
    [SerializeField] ThirdPersonCamera camScript;
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] worldItemsArray;
    [SerializeField] GameObject[] inventoryItemsArray;

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

    bool openMode = false;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void AddItem(GameObject AddedItem)
    {
        foreach (GameObject item in inventoryItemsArray)
        {
            if ((AddedItem.name + " 2D") == item.name)
            {
                GameObject newItem = Instantiate(item, spawnPoint.transform.position, Quaternion.identity, bag.transform);

                newItem.name = item.name;

                break;
            }
        }
    }

    public void DropItem(GameObject item)
    {
        foreach (GameObject worldItem in worldItemsArray)
        {
            string worldName = item.name.Remove(item.name.Length - 3, 3);

            if (worldName == worldItem.name)
            {
                Vector3 spawnDistance = 1.5f * player.transform.forward;

                GameObject newItem = Instantiate(worldItem, player.transform.position + spawnDistance, Quaternion.identity);

                newItem.name = worldName;

                Destroy(item);

                break;
            }
        }
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
        if (camScript.currentMode == ThirdPersonCamera.CameraMode.Exploration)
        {
            if (Input.GetButtonDown("Inventory"))
            {
                if (bagRectTransform.localScale.x > smallScale.x)
                {
                    bagRectTransform.localScale = smallScale;
                    bagRectTransform.anchoredPosition = smallPosit;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }

                else
                {
                    bagRectTransform.localScale = bigScale;
                    bagRectTransform.anchoredPosition = bigPosit;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }

        else
        {
            if (bagRectTransform.localScale.x > smallScale.x)
            {
                bagRectTransform.localScale = smallScale;
                bagRectTransform.anchoredPosition = smallPosit;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        

        if (Input.GetButtonDown("Drop"))
        {
            
        }
    }

    #endregion
    //========================


}
