using System.Collections;
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

    float dampner = 10; //how much the intensity will be divided for when applying stats to objects in the tornado

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

    IEnumerator pullObject(Collider objCollider, bool shouldPull)
    {
        if (shouldPull)
        {
            //pull objects with rigid body
            Vector3 forceDirection = tornadoCenter.position - objCollider.transform.position;
            float actualPullForce = pullForce * transform.localScale.x;

            objCollider.GetComponent<Rigidbody>().AddForce(forceDirection * actualPullForce * Time.deltaTime);

            //aply stats if needed
            StatsManager objStats;
            objCollider.gameObject.TryGetComponent<StatsManager>(out objStats);

            if (tornadoType == ElementalType.Ice | tornadoType == ElementalType.Mixed)
            {   
                if (objStats != null)
                {
                    objStats.ApplyStatSelf(StatsConst.ICE, selfStats.ice[StatsConst.SELF_INTENSITY]/dampner, selfStats.ice[StatsConst.APPLY_REACH_TIME], selfStats.ice[StatsConst.APPLY_RETURN_TIME]);
                }
            }

            if (tornadoType == ElementalType.Fire | tornadoType == ElementalType.Mixed)
            {   
                if (objStats != null)
                {
                    objStats.ApplyStatSelf(StatsConst.FIRE, selfStats.fire[StatsConst.SELF_INTENSITY]/dampner, selfStats.fire[StatsConst.APPLY_REACH_TIME], selfStats.fire[StatsConst.APPLY_RETURN_TIME]);
                }
            }

            //wait and restart coroutine
            yield return new WaitForSeconds(waitTime);
            
            StartCoroutine(pullObject(objCollider, true));
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<Rigidbody>() != null)
        {
            StartCoroutine(pullObject(collider, true));
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.GetComponent<Rigidbody>() != null)
        {
            StartCoroutine(pullObject(collider, false));
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
