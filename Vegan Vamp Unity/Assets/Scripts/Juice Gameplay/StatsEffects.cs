using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

//VFX container gotta be the third object

public class StatsEffects : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    StatsManager selfStats;

    //FX
    GameObject fire;

    //objects
    [SerializeField] GameObject iceCube1;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    //Consts
    const int BASE_INTENSITY = 0;
    const int SELF_INTENSITY = 1;
    const int SELF_DURATION = 2;
    const int APPLY_INTENSITY = 3;
    const int APPLY_DURATION = 4;
    const int CAP_INTENSITY = 5;
    const int CAP_DURATION = 6;
    const int STARTING_INTENSITY = 7;
    const int PASSED_TIME = 8;

    //Stats order
    const int FIRE = 0;

    //Check what coroutine is running
    bool burning;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region



    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        selfStats = GetComponent<StatsManager>();

        //Get vfx objects
        Transform VFX = transform.GetChild(2);

        fire = VFX.GetChild(FIRE).gameObject;
    }

    void Update()
    {
        //FIRE
        if (selfStats.burning[SELF_INTENSITY] > 0)
        {
            if (!fire.activeSelf)
            {
                fire.SetActive(true);
            }

            float fireScale = selfStats.burning[SELF_INTENSITY];

            fire.transform.localScale = new Vector3(fireScale, fireScale, fireScale);
        }

        else
        {
            if (fire.activeSelf)
            {
                fire.SetActive(false);
            }
        }

        //ICE
        if (selfStats.chilling[SELF_INTENSITY] > 0)
        {
            if (!iceCube1.activeSelf)
            {
                iceCube1.SetActive(true);
            }

            float iceScale = selfStats.chilling[SELF_INTENSITY];

            iceCube1.transform.localScale = new Vector3(iceScale, iceScale, iceScale);
        }

        else
        {
            if (iceCube1.activeSelf)
            {
                iceCube1.SetActive(false);
            }
        }
    }

    #endregion
    //========================


}
