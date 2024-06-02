using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Fire : MonoBehaviour
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

    List<GameObject> burningObjs = new List<GameObject>();

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator BurnObject(GameObject obj, StatsManager objStats)
    {
        if (burningObjs.Contains(obj))
        {
            objStats.ApplyStatSelf(StatsConst.FIRE, 0.1f, 0.5f, 0.6f);

            // Wait and restart coroutine
            yield return new WaitForSeconds(0.05f);

            StartCoroutine(BurnObject(obj, objStats));
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
                if (!burningObjs.Contains(collider.gameObject))
                {
                    burningObjs.Add(collider.gameObject);
                    StartCoroutine(BurnObject(collider.gameObject, objStats));
                    print(collider.name);
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (burningObjs.Contains(collider.gameObject))
        {
            burningObjs.Remove(collider.gameObject);
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
