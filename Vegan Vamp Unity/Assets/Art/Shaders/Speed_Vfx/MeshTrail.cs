using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float activeTime = 2f;

    [Header("Mesh Related")]
    public float meshRefrashRate = 0.1f;

    private bool isTrailActive;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !isTrailActive)
        {
            isTrailActive = true;
            StartCoroutine(ActiveTrail(activeTime));
        }
    }

    IEnumerator ActiveTrail (float timeActive)
    {
        while (activeTime > 0)
        {
            timeActive -= meshRefrashRate;

            

            yield return new WaitForSeconds(meshRefrashRate);
        }

        isTrailActive = false;
    }
}
