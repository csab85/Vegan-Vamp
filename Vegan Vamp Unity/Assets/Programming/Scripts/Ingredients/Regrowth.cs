using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Regrowth : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //game objects
    [SerializeField] GameObject[] ingredients;

    //components
    List<Collider> collidersList = new List<Collider>();

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] float[] timers;
    [SerializeField] float growthTime;
    [SerializeField] float harvestSize;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void Grow(Transform ingredient, float passedTime)
    {
        float sizePercentage = Mathf.Clamp01(passedTime / growthTime);

        float scale = Mathf.Lerp(0.01f, 1, sizePercentage);

        ingredient.transform.localScale = new Vector3(scale, scale, scale);
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        foreach (GameObject ingredient in ingredients)
        {
            collidersList.Add(ingredient.GetComponent<Collider>());
        }
    }

    void Update()
    {
        for (int i = 0; i < ingredients.Length; i++)
        {
            GameObject ingredient = ingredients[i];
            Collider selfCollider = collidersList[i];

            //set if harvestable
            if (ingredient.transform.localScale.y >= harvestSize)
            {
                if (ingredient.tag == "Ingredient")
                {
                    selfCollider.enabled = true;
                }
            }

            else
            {
                selfCollider.enabled = false;
            }

            //grow
            if (ingredient.transform.localScale.y < 1)
            {
                Grow(ingredient.transform, timers[i]);
                timers[i] += Time.deltaTime;
            }

            else
            {
                timers[i] = 0;
            }
        }
    }


    #endregion
    //========================


}
