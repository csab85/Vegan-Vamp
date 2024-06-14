using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class TeleportShroom : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] VisualEffect particles;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] public float areaRadius;
    [SerializeField] float tpCooldown;
    [HideInInspector] public Vector3 areaCenter;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator RandomTeleport()
    {
        float angle = Random.Range(0, Mathf.PI * 2);
        float distance = Random.Range(0, areaRadius);

        float circleX = areaCenter.x + Mathf.Cos(angle) * distance;
        float circleZ = areaCenter.z + Mathf.Sin(angle) * distance;

        transform.position = new Vector3(circleX, 0, circleZ);
        particles.Play();

        yield return new WaitForSeconds(tpCooldown);

        StartCoroutine(RandomTeleport());
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        areaCenter = transform.position;
        StartCoroutine(RandomTeleport());
    }

    #endregion
    //========================


}
