using System.Collections;
using UnityEngine;

public class TornadoFruit : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    GameObject miniTornado;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] float miniTornadoCooldown;

    [HideInInspector] public bool itsTornadoTime = true;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public IEnumerator ActivateMiniTornado()
    {
        itsTornadoTime = false;
        yield return new WaitForSeconds(miniTornadoCooldown);
        miniTornado.SetActive(true);
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        miniTornado = transform.Find("VFX Mini Tornado").gameObject;
    }

    void Update()
    {
        if (itsTornadoTime)
        {
            StartCoroutine(ActivateMiniTornado());
        }
    }

    #endregion
    //========================


}
