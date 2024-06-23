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

    [Header("Imports")]
    [SerializeField] ThirdPersonCamera camScript;
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] worldItemsArray;
    [SerializeField] GameObject[] inventoryItemsArray;
    [SerializeField] Transform[] spawnpoints;

    GameObject bag;
    RectTransform bagRectTransform;
    StatsManager playerStats;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Settings")]
    [SerializeField] float bigScale;
    [SerializeField] Vector2 bigPosit;

    int spawnCounter = 0;

    Vector2 basePosit;
    Vector2 baseScale;
    public bool openMode = false;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void AddItem(GameObject addedItem)
    {
        foreach (GameObject item in inventoryItemsArray)
        {
            if ((addedItem.name + " 2D") == item.name)
            {
                GameObject newItem = Instantiate(item, spawnpoints[spawnCounter].position, Quaternion.identity, bag.transform);

                spawnCounter = (spawnCounter + 1) % spawnpoints.Length;

                newItem.name = item.name;

                if (item.tag == "Juice")
                {
                    addedItem.GetComponent<StatsManager>().PasteStats(newItem.GetComponent<StatsManager>());
                }

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

                newItem.GetComponent<Rigidbody>().AddForce(newItem.transform.forward, ForceMode.Impulse);

                if (item.tag == "Juice")
                {
                    item.GetComponent<StatsManager>().PasteStats(newItem.GetComponent<StatsManager>());
                }


                Destroy(item);

                break;
            }
        }
    }

    static GameObject FindChild(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.gameObject.name == name)
            {
                return child.gameObject;
            }
        }

        return null;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        //game objects
        bag = FindChild(transform, "Bag");

        //components
        bagRectTransform = bag.GetComponent<RectTransform>();
        playerStats = player.GetComponent<StatsManager>();

        //values
        basePosit = bagRectTransform.anchoredPosition;
        baseScale = bagRectTransform.localScale;
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory") && !playerStats.dead)
        {
            if (openMode)
            {
                bagRectTransform.localScale = baseScale;
                bagRectTransform.anchoredPosition = basePosit;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                openMode = false;

                camScript.explorationCamera.SetActive(true);
                camScript.combatCamera.SetActive(true);
            }

            else
            {
                bagRectTransform.localScale = new Vector3(bigScale, bigScale, bigScale);
                bagRectTransform.anchoredPosition = bigPosit;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                openMode = true;
                camScript.explorationCamera.SetActive(false);
                camScript.combatCamera.SetActive(false);
            }
        }
    }

    #endregion
    //========================


}
