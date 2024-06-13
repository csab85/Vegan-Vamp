using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class HealAura : MonoBehaviour
{
    //IMPORTS
    //========================
    #region
    
    //game objetcs
    [SerializeField] GameObject healIngredient;

    //compents
    VisualEffect healVFX;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] float healPower;
    [SerializeField] float healFrequency;

    StatsManager.Type[] allowedTypes = {StatsManager.Type.Ingredient, StatsManager.Type.NPC, StatsManager.Type.Player};

    List<GameObject> healingObjs = new List<GameObject>();

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator HealObject(GameObject obj, StatsManager objStats)
    {
        if (healingObjs.Contains(obj))
        {
            objStats.ApplyToBase(StatsConst.HEALTH, healPower);

            // Wait and restart coroutine
            yield return new WaitForSeconds(healFrequency);

            StartCoroutine(HealObject(obj, objStats));
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (healVFX.enabled)
        {
            StatsManager objStats = null;

            //check if it has a stats manager
            if (collider.gameObject.TryGetComponent<StatsManager>(out objStats))
            {
                //check if it is one of the allowed types
                if (allowedTypes.Contains(objStats.objectType))
                {
                    //check if collider is in the dictionary
                    if (!healingObjs.Contains(collider.gameObject))
                    {
                        healingObjs.Add(collider.gameObject);
                        StartCoroutine(HealObject(collider.gameObject, objStats));
                    }
                }
            }
        }

    }

    void OnTriggerExit(Collider collider)
    {
        if (healingObjs.Contains(collider.gameObject))
        {
            healingObjs.Remove(collider.gameObject);
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        //get components
        healVFX = GetComponent<VisualEffect>();
    }

    void Update()
    {
        //change it to when its below a size, for when I implement growth
        
        if (!healIngredient.activeSelf)
        {
            healVFX.enabled = false;

            if (healingObjs.Count > 0)
            {
                foreach (GameObject obj in healingObjs)
                {
                    healingObjs.Remove(obj);
                }
            }
        }

        else if(!healVFX.enabled)
        {
            healVFX.enabled = true;
        }
    }

    #endregion
    //========================


}
