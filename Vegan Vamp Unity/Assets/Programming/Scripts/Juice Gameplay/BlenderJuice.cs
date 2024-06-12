using System;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using System.Diagnostics;

public class BlenderJuice : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //game objects
    [SerializeField] GameObject baseJuice;
    GameObject blender;

    //scripts
    [SerializeField] Inventory inventory;

    //materials
    Material juiceMaterial;

    StatsManager selfStats;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    Color initialColor;

    [SerializeField] int maxBottles;
    [SerializeField] float maxFilling;
    [SerializeField] float minFilling;

    float targetFill;
    float fill;
    bool filling;

    List<GameObject> ingredientsInside = new List<GameObject>{};

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ingredient"))
        {   
            ingredientsInside.Add(collider.gameObject);
            StatsManager ingredientStats = collider.gameObject.GetComponent<StatsManager>();

            //get only apply values
            for (int i = 0; i < selfStats.statsArray.Count(); i++)
            {
                selfStats.AddToSelfApply(i, ingredientStats.statsArray[i][StatsConst.APPLY_INTENSITY], ingredientStats.statsArray[i][StatsConst.APPLY_REACH_TIME], ingredientStats.statsArray[i][StatsConst.APPLY_RETURN_TIME]);
            }

            //reset if only color is initial white
            if (selfStats.colors[0] == initialColor)
            {
                selfStats.colors = new List<Color>();
            }            

            foreach (Color color in ingredientStats.colors)
            {
                selfStats.colors.Add(color);
            }
        }
    }

    public void BlendJuice()
    {
        //blend ingredients
        if (ingredientsInside.Count > 0)
        {
            foreach (GameObject ingredient in ingredientsInside)
            {
                Destroy(ingredient);
            }
        }

        ingredientsInside = new List<GameObject>();

        filling = true;
    }

    public void FillBottle()
    {
        if (!filling)
        {
            //spawn and set juice stats
            GameObject newJuice = Instantiate(baseJuice, transform.position, Quaternion.identity);
            newJuice.name = "Juice Bottle";

            newJuice.GetComponent<StatsManager>().CopyStats(selfStats);

            //add it to inventory
            inventory.AddItem(newJuice);

            //DESTROY new juice
            Destroy(newJuice);

            //drain juice
            targetFill -= (maxFilling + Mathf.Abs(minFilling))/maxBottles;

        }

        //reset juice if empty
        if (fill <= -2)
        {
            //reset stats
            foreach (float[] stat in selfStats.statsArray)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (stat[i] != 0)
                    {
                        stat[i] = 0;
                    }
                }
            }

            //reset to default color
            selfStats.colors = new List<Color>() {initialColor, Color.green};
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        //get game objects
        blender = transform.parent.gameObject;

        // get scripts
        selfStats = GetComponent<StatsManager>();

        //get materials
        juiceMaterial = GetComponent<Renderer>().material;

        //get initial color
        initialColor = selfStats.colors[0];

        fill = minFilling;
        targetFill = minFilling;
    }

    void Update()
    {
        //manage filling
        if (filling)
        {
            if (targetFill < maxFilling)
            {
                targetFill = Mathf.MoveTowards(targetFill, maxFilling, Time.deltaTime * 5);
            }

            else
            {
                filling = false;
            }
        }
    
        //update material
        if (fill != targetFill)
        {
            fill = Mathf.MoveTowards(fill, targetFill, Time.deltaTime * 5);
        }

        if (juiceMaterial.GetFloat("_fill") != fill)
        {
            juiceMaterial.SetFloat("_fill", fill);
        }

        //set if juice interactable or not
        if (fill > minFilling)
        {
            if (gameObject.layer != LayerMask.NameToLayer("Interactable"))
            {
                gameObject.layer = LayerMask.NameToLayer("Interactable");
            }
        }

        else
        {
            if (gameObject.layer == LayerMask.NameToLayer("Interactable"))
            {
                gameObject.layer = LayerMask.NameToLayer("Default");
            }
        }

        //set if blender interactable or not
        if (ingredientsInside.Count > 0)
        {
            if (blender.layer != LayerMask.NameToLayer("Interactable"))
            {
                blender.layer = LayerMask.NameToLayer("Interactable");
            }
        }

        else
        {
            if (blender.layer == LayerMask.NameToLayer("Interactable"))
            {
                blender.layer = LayerMask.NameToLayer("Default");
            }
        }
    }

    #endregion
    //========================


}
