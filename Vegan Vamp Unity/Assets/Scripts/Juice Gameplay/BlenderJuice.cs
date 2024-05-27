using System;
using System.Linq;
using System.Collections.Generic;
using System.Transactions;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class BlenderJuice : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] Material juiceMaterial;
    [SerializeField] GameObject baseJuice;

    StatsManager selfStats;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    List<GameObject> ingredientsInside = new List<GameObject>{};

    const int BASE_INTENSITY = 0;
    const int SELF_INTENSITY = 1;
    const int SELF_DURATION = 2;
    const int APPLY_INTENSITY = 3;
    const int APPLY_DURATION = 4;
    const int CAP_INTENSITY = 5;
    const int CAP_DURATION = 6;
    const int STARTING_INTENSITY = 7;
    const int PASSED_TIME = 8;

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

            //get only apply values (see if I cant get em all. Prolly not)
            for (int i = 0; i < selfStats.statsArray.Count(); i++)
            {
                //FIX THIS
                //selfStats.AddToSelfApply(i, ingredientStats.statsArray[i][APPLY_INTENSITY], ingredientStats.statsArray[i][APPLY_DURATION]);
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
        

        //spawn and set juice stats
        Vector3 spawnPoint = transform.position + new Vector3(0, 1, 0);
        GameObject newJuice = Instantiate(baseJuice, spawnPoint, Quaternion.identity, null);

        for (int i = 0; i < selfStats.statsArray.Count(); i++)
        {
            for(int j = 0; j < 9; j++)
            {
                newJuice.GetComponent<StatsManager>().statsArray[i][j] = selfStats.statsArray[i][j];
            }
        }

        newJuice.layer = LayerMask.NameToLayer("Interactable");
    
        //throw juice from blender
        Vector3 throwDirection = (transform.forward + transform.up) * 5;
        newJuice.GetComponent<JuiceBottle>().Intact.SetActive(true);
        newJuice.GetComponent<BoxCollider>().enabled = true;
        newJuice.GetComponent<Rigidbody>().isKinematic = false;
        newJuice.GetComponent<Rigidbody>().AddForce(throwDirection, ForceMode.Impulse);



        //reset juice stats
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
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        selfStats = GetComponent<StatsManager>();
    }

    #endregion
    //========================


}
