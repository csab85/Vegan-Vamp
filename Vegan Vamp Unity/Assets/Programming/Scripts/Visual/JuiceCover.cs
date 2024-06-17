using UnityEngine;

public class JuiceCover : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //game objects
    [SerializeField] GameObject blenderJuiceObj;

    //materials
    Material selfJuice;
    Material blenderJuice;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region



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
        selfJuice = GetComponent<Renderer>().material;
        blenderJuice = blenderJuiceObj.GetComponent<Renderer>().material;
    }

    void Update()
    {
        print(blenderJuice.GetColor("_Fresnel_Color2"));
        selfJuice.SetColor("_Fresnel_Color2", blenderJuice.GetColor("_Fresnel_Color2"));
        selfJuice.SetColor("_Surface_Color", blenderJuice.GetColor("_Surface_Color"));
    }

    #endregion
    //========================


}
