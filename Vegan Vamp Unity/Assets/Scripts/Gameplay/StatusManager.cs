using System;
using System.Diagnostics.SymbolStore;
using UnityEngine;

public class StatusManager : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] Stats.Dirty oie = new Stats.Dirty();

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Serializable]
    public class Stats
    {
        [Serializable]
        public class Dirty
        {
            public float intensity;
            public float duration;
        }
    }

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

    void Update()
    {
        Stats.Dirty oie = new Stats.Dirty();
    }

    #endregion
    //========================


}
