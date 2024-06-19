using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.VFX;

public class Tornado : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] Transform tornadoCenter;
    [SerializeField] float pullForce;
    [SerializeField] float waitTime;
    [SerializeField] float colorRefreshRate;
    [SerializeField] Color iceColor;
    [SerializeField] float iceColorIntensity;
    [SerializeField] Color fireColor;
    [SerializeField] float fireColorIntensity;
 
    Color actualColor1;
    Color actualColor2;
    StatsManager selfStats;
    VisualEffect vfx;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    StatsManager.Type[] allowedTypes = { StatsManager.Type.Ingredient, StatsManager.Type.NPC, StatsManager.Type.Player };

    List<GameObject> pullingObjs = new List<GameObject>();
    List<GameObject> burningObjs = new List<GameObject>();
    List<GameObject> freezingObjs = new List<GameObject>();

    enum ElementalType
    {
        Normal,
        Ice,
        Fire,
        Mixed
    }

    ElementalType tornadoType;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator PullObject(GameObject obj)
    {
        if (pullingObjs.Contains(obj))
        {
            //pull objects with rigid body
            Vector3 forceDirection = tornadoCenter.position - obj.transform.position;
            float actualPullForce = pullForce * transform.localScale.x;

            obj.GetComponent<Rigidbody>().AddForce(forceDirection * actualPullForce * Time.deltaTime);

            yield return new WaitForSeconds(0.05f);

            StartCoroutine(PullObject(obj));
        }
    }

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

    IEnumerator FreezeObject(GameObject obj, StatsManager objStats)
    {
        if (freezingObjs.Contains(obj))
        {
            objStats.ApplyStatSelf(StatsConst.ICE, 0.1f, 0.5f, 0.6f);

            // Wait and restart coroutine
            yield return new WaitForSeconds(0.05f);

            StartCoroutine(FreezeObject(obj, objStats));
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        //check if has rigid body
        Rigidbody rb = null;
        collider.TryGetComponent<Rigidbody>(out rb);

        if (rb != null)
        {
            //check if collider is in the dictionary
            if (!pullingObjs.Contains(collider.gameObject))
            {
                pullingObjs.Add(collider.gameObject);
                StartCoroutine(PullObject(collider.gameObject));
            }
        }

        //check if it has a stats manager
        StatsManager objStats = null;
        collider.gameObject.TryGetComponent<StatsManager>(out objStats);

        if (objStats != null)
        {
            //check if it is one of the allowed types
            if (allowedTypes.Contains(objStats.objectType))
            {
                //check tornado elemental type
                if (tornadoType == ElementalType.Fire | tornadoType == ElementalType.Mixed)
                {
                    //check if collider is in the dictionary
                    if (!burningObjs.Contains(collider.gameObject))
                    {
                        burningObjs.Add(collider.gameObject);
                        StartCoroutine(BurnObject(collider.gameObject, objStats));
                    }
                }

                if (tornadoType == ElementalType.Ice | tornadoType == ElementalType.Mixed)
                {
                    //check if collider is in the dictionary
                    if (!freezingObjs.Contains(collider.gameObject))
                    {
                        freezingObjs.Add(collider.gameObject);
                        StartCoroutine(FreezeObject(collider.gameObject, objStats));
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (pullingObjs.Contains(collider.gameObject))
        {
            pullingObjs.Remove(collider.gameObject);
        }

        if (burningObjs.Contains(collider.gameObject))
        {
            burningObjs.Remove(collider.gameObject);
        }

        if (freezingObjs.Contains(collider.gameObject))
        {
            freezingObjs.Remove(collider.gameObject);
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        actualColor1 = Color.white;
        actualColor2 = Color.white;
        selfStats = GetComponent<StatsManager>();
        vfx = GetComponent<VisualEffect>();

        iceColor *= iceColorIntensity;
        fireColor *= fireColorIntensity;
    }

    void Update()
    {
        if (selfStats.tornado[StatsConst.SELF_INTENSITY] > 0)
        {
            //set size
            float selfScale = selfStats.tornado[StatsConst.SELF_INTENSITY];

            transform.localScale = new Vector3(selfScale, selfScale, selfScale);

            //do thingies for elemental tornados
            
            if (selfStats.fire[StatsConst.SELF_INTENSITY] > 0 && selfStats.ice[StatsConst.SELF_INTENSITY] > 0)
            {
                tornadoType = ElementalType.Mixed;

                if (actualColor1 != iceColor | actualColor2 != fireColor)
                {
                    actualColor1 = Vector4.MoveTowards(actualColor1, iceColor, colorRefreshRate);
                    actualColor2 = Vector4.MoveTowards(actualColor2, fireColor, colorRefreshRate);
                }
            }

            else if (selfStats.ice[StatsConst.SELF_INTENSITY] > 0)
            {
                tornadoType = ElementalType.Ice;

                if (actualColor1 != iceColor)
                {
                    actualColor1 = Vector4.MoveTowards(actualColor1, iceColor, colorRefreshRate);
                    actualColor2 = Vector4.MoveTowards(actualColor2, iceColor, colorRefreshRate);
                }
            }

            else if (selfStats.fire[StatsConst.SELF_INTENSITY] > 0)
            {
                tornadoType = ElementalType.Fire;

                if (actualColor1 != fireColor)
                {
                    actualColor1 = Vector4.MoveTowards(actualColor1, fireColor, colorRefreshRate);
                    actualColor2 = Vector4.MoveTowards(actualColor2, fireColor, colorRefreshRate);
                }
            }

            else
            {
                tornadoType = ElementalType.Normal;

                if (actualColor1 != Color.white)
                {
                    actualColor1 = Vector4.MoveTowards(actualColor1, Color.white, colorRefreshRate);
                    actualColor2 = Vector4.MoveTowards(actualColor2, Color.white, colorRefreshRate);
                }
            }

            //update colors
            int tornadoColor1 = Shader.PropertyToID("Tornado Color 1");
            int tornadoColor2 = Shader.PropertyToID("Tornado Color 2");
            vfx.SetVector4(tornadoColor1, actualColor1);
            vfx.SetVector4(tornadoColor2, actualColor2);
        }

        else
        {
            Destroy(gameObject);
        }
    }

    #endregion
    //========================


}
