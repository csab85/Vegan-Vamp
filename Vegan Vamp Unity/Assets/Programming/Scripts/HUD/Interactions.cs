using System.Collections.Specialized;
using System.Data;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.Rendering;

public class Interactions : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header ("Imports")]
    [SerializeField] Inventory inventory;
    [SerializeField] GameObject waterBottle;
    [SerializeField] public GameObject player;

    BlenderJuice blenderJuice;
    GameObject interactionUI;
    GameObject interactText; 
    Collider[] nearbyInteractions;
    StatsManager playerStats;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Settings")]
    [SerializeField] public float interactionRange;
    [SerializeField] LayerMask targetLayers;
    [SerializeField] Vector2 textOffset;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void Interact(GameObject interactObj)
    {
        if (interactObj.layer == LayerMask.NameToLayer("Ingredient"))
        {
            interactObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            inventory.AddItem(interactObj);
        }

        if (interactObj.name == "Blender")
        {
            blenderJuice.BlendJuice();
        }

        if (interactObj.name == "Blender Juice")
        {
            blenderJuice.FillBottle();
        }

        if (interactObj.tag == "Juice")
        {
            Destroy(interactObj);
            inventory.AddItem(interactObj);
        }

        if (interactObj.tag == "Water")
        {
            GameObject newWaterBottle = Instantiate(waterBottle);
            newWaterBottle.name = "Water Bottle";
            inventory.AddItem(newWaterBottle);
            Destroy(newWaterBottle);
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {   
        interactionUI = transform.GetChild(0).gameObject;
        interactText = interactionUI.transform.GetChild(0).gameObject;
        playerStats = player.GetComponent<StatsManager>();
        blenderJuice = GameObject.Find("Blender Juice").GetComponent<BlenderJuice>();
    }

    void Update()
    {
        nearbyInteractions = Physics.OverlapSphere(player.transform.position, interactionRange, targetLayers);

        if (nearbyInteractions.Length > 0)
        {
            //get nearest ingredient
            GameObject interactObject = nearbyInteractions[0].gameObject;

            //get where on screen the ingredient is (origin bottom left)
            Vector2 pointOnScreen = Camera.main.WorldToScreenPoint(nearbyInteractions[0].ClosestPoint(player.transform.position));
            
            if (pointOnScreen.x > 0 && pointOnScreen.y > 0)
            {
                //show ui over object
                interactionUI.SetActive(true);
                interactText.GetComponent<RectTransform>().position = pointOnScreen + textOffset;

                //get if interact button is pressed
                if (Input.GetButtonDown("Interact") && !playerStats.dead)
                {
                    if (playerStats.ice[StatsConst.SELF_INTENSITY] <= 0)
                    {
                        Interact(interactObject);
                    }
                }
            }
        }

        else
        {
            interactionUI.SetActive(false);
        }
    }

    #endregion
    //========================


}
