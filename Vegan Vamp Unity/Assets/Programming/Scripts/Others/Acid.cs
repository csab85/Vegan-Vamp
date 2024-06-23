using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Acid: MonoBehaviour
{
    //IMPORTS
    //========================
    #region



    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    StatsManager.Type[] allowedTypes = { StatsManager.Type.Ingredient, StatsManager.Type.NPC, StatsManager.Type.Player };

    List<GameObject> meltingObjs = new List<GameObject>();

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator MeltObject(GameObject obj, StatsManager objStats)
    {
        if (meltingObjs.Contains(obj))
        {
            objStats.ApplyToBase(StatsConst.HEALTH, -0.3f);

            //vignette thingy
            StatsEffects targetEffect = obj.GetComponent<StatsEffects>();

            StartCoroutine(targetEffect.DamageVignette());

            // Wait and restart coroutine
            yield return new WaitForSeconds(0.05f);

            StartCoroutine(MeltObject(obj, objStats));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //stop falling when touching ground
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
        }

        //apply damage
        StatsManager objStats = null;

        //check if it has a stats manager
        if (other.gameObject.TryGetComponent<StatsManager>(out objStats))
        {
            //check if it is one of the allowed types
            if (allowedTypes.Contains(objStats.objectType))
            {
                //check if collider is in the dictionary
                if (!meltingObjs.Contains(other.gameObject))
                {
                    meltingObjs.Add(other.gameObject);
                    StartCoroutine(MeltObject(other.gameObject, objStats));
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (meltingObjs.Contains(collider.gameObject))
        {
            meltingObjs.Remove(collider.gameObject);
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
