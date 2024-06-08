using System.Collections.Specialized;
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
    [SerializeField] BlenderJuice blenderJuice;
    [SerializeField] JuiceBottle juiceBottle;
    [SerializeField] public GameObject player;
    
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
            interactObj.SetActive(false);
            inventory.AddItem(interactObj);
        }

        if (interactObj.name == "Blender")
        {
            blenderJuice.BlendJuice();
        }

        if (interactObj.name == "Base Juice(Clone)")
        {
            juiceBottle.GrabJuice(interactObj);
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
    }

    void Update()
    {
        nearbyInteractions = Physics.OverlapSphere(player.transform.position, interactionRange, targetLayers);

        if (nearbyInteractions.Length > 0)
        {
            //get nearest ingredient
            GameObject interactObject = nearbyInteractions[0].gameObject;

            //get where on screen the ingredient is (origin bottom left)
            Vector2 pointOnScreen = Camera.main.WorldToScreenPoint(interactObject.transform.position);
            
            if (pointOnScreen.x > 0 && pointOnScreen.y > 0)
            {
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
