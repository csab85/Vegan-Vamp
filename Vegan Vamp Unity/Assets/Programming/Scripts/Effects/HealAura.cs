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
    StatsEffects playerEffects;
    [SerializeReference] VisualEffect healBurst;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] float healPower;
    [SerializeField] float healFrequency;
    float baseHealPower;

    StatsManager.Type[] allowedTypes = {StatsManager.Type.Player};

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

            //vignette
            playerEffects.vignetteCorrect = false;
            playerEffects.vignetteColor = Color.green;
            playerEffects.vignetteIntensity = 0.4f;

            // Wait and restart coroutine
            yield return new WaitForSeconds(healFrequency);

            StartCoroutine(HealObject(obj, objStats));

            yield break;
        }

        playerEffects.vignetteCorrect = false;
        playerEffects.vignetteIntensity = 0;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (healVFX.enabled)
        {
            StatsManager objStats = null;
            collider.gameObject.TryGetComponent<StatsManager>(out objStats);

            //check if it has a stats manager
            if (objStats != null)
            {
                //check if it is one of the allowed types
                if (allowedTypes.Contains(objStats.objectType))
                {
                    //check if collider is in the dictionary
                    if (!healingObjs.Contains(collider.gameObject))
                    {
                        healingObjs.Add(collider.gameObject);
                        StartCoroutine(HealObject(collider.gameObject, objStats));

                        //effect
                        healBurst.gameObject.transform.position = collider.transform.position;
                        healBurst.Play();
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

        //get values
        baseHealPower = healPower;

        //get scripts
        playerEffects = GameObject.Find("Player").GetComponent<StatsEffects>();
    }

    void Update()
    {
        //update size and intensity
        Vector3 healScale = healIngredient.transform.localScale;
        transform.localScale = new Vector3(healScale.x * 1.5f, healScale.y, healScale.z * 1.5f);
        
        if (healPower != transform.localScale.x * baseHealPower)
        {
            healPower = transform.localScale.x * baseHealPower;
        }

    }

    #endregion
    //========================


}
