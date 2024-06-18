using UnityEngine;

public class TransitionCollider: MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //game objects
    [SerializeField] GameObject bag;
    [SerializeField] GameObject hotbar;

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

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Juice" | collider.tag == "Ingredient")
        {
            if (collider.transform.parent.gameObject != bag)
            {
                collider.transform.SetParent(bag.transform);
            }

            //fix bottle icon changing size
            if (collider.tag == "Juice")
            {
                if (collider.GetComponent<BottleIcon>().placeInBag == BottleIcon.Place.None)
                {
                    collider.GetComponent<BottleIcon>().placeInBag = BottleIcon.Place.Outside;
                }
            }

        }
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Juice" | collider.tag == "Ingredient")
        {
            if (collider.transform.parent.gameObject != hotbar)
            {
                collider.transform.SetParent(hotbar.transform);
                collider.transform.SetSiblingIndex(2);
            }

            if (collider.tag == "Juice")
            {
                //fix bottle icon changing size
                collider.GetComponent<BottleIcon>().placeInBag = BottleIcon.Place.None;
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
