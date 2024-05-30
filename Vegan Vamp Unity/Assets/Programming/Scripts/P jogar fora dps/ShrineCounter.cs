using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrineCounter : MonoBehaviour
{
    [SerializeField] GameObject portal;
    static int counter = 0;

    void OnCollisionEnter(Collision collision)
    {
        counter += 1;
        Destroy(gameObject);
    }

    void Update()
    {
        if (counter >= 12)
        {
            portal.SetActive(true);
        }
    }
}
