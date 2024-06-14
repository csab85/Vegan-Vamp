using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Water : MonoBehaviour
{
    //IMPORTS
    //========================
    #region



    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    StatsManager.Type[] allowedTypes = {StatsManager.Type.Ingredient, StatsManager.Type.NPC, StatsManager.Type.Player};

    List<GameObject> wetObjs = new List<GameObject>();

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator WetObject(GameObject obj, StatsManager objStats)
    {
        if (wetObjs.Contains(obj))
        {
            objStats.ApplyStatSelf(StatsConst.FIRE, -0.1f, 0.5f, 0.6f);

            // Wait and restart coroutine
            yield return new WaitForSeconds(0.05f);

            StartCoroutine(WetObject(obj, objStats));
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        StatsManager objStats = null;

        //check if it has a stats manager
        if (collider.gameObject.TryGetComponent<StatsManager>(out objStats))
        {
            //check if it is one of the allowed types
            if (allowedTypes.Contains(objStats.objectType))
            {
                //check if collider is in the dictionary
                if (!wetObjs.Contains(collider.gameObject))
                {
                    wetObjs.Add(collider.gameObject);
                    StartCoroutine(WetObject(collider.gameObject, objStats));
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (wetObjs.Contains(collider.gameObject))
        {
            wetObjs.Remove(collider.gameObject);
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
