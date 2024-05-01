using Unity.VisualScripting;
using UnityEngine;

public class GrapeBullet : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject parent;
    MainCamera mainCameraScript;
    Gun grapeShooterScript;
    

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] float maxDistance;
    Vector3 target;
    public bool targeted = false;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void OnCollisionEnter(Collision collision)
    {
        transform.parent = parent.transform;
        transform.localPosition = Vector3.zero;
        targeted = false;
        gameObject.SetActive(false);
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        mainCameraScript = GameObject.Find("Main Camera").GetComponent<MainCamera>();
        grapeShooterScript = GameObject.Find("Grape Shooter").GetComponent<Gun>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, parent.transform.position) > maxDistance)
        {
            transform.parent = parent.transform;
            transform.localPosition = Vector3.zero;
            targeted = false;
            gameObject.SetActive(false);
        }

        if (!targeted)
        {
            target = mainCameraScript.aimHit.point;
            targeted = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, grapeShooterScript.shotPower * Time.deltaTime);
    }

    #endregion
    //========================


}
