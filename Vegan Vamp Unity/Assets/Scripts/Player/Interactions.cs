using System.Collections.Specialized;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class Interactions : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header ("Imports")]
    [SerializeField] LayerMask targetLayers;
    [SerializeField] GameObject interactUI;

    Collider[] nearbyInteractions;
    GameObject interactText; 
    Inventory inventory;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Settings")]
    [SerializeField] public float interactionRange;
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
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        interactText = interactUI.transform.GetChild(0).gameObject;
        inventory = GetComponent<Inventory>();
    }

    void Update()
    {
        nearbyInteractions = Physics.OverlapSphere(transform.position, interactionRange, targetLayers);

        if (nearbyInteractions.Length > 0)
        {
            //get nearest ingredient
            GameObject interactObject = nearbyInteractions[0].gameObject;

            //get where on screen the ingredient is (origin bottom left)
            Vector2 pointOnScreen = Camera.main.WorldToScreenPoint(interactObject.transform.position);
            
            if (pointOnScreen.x > 0 && pointOnScreen.y > 0)
            {
                interactUI.SetActive(true);
                interactText.GetComponent<RectTransform>().position = pointOnScreen + textOffset;

                //get if interact button is pressed
                if (Input.GetButtonDown("Interact"))
                {
                    Interact(interactObject);
                }
            }
        }

        else
        {
            interactUI.SetActive(false);
        }
    }

    #endregion
    //========================


}
