using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

[SelectionBase]
public class BreakBeattle : MonoBehaviour
{
    [SerializeField] GameObject Intact;
    [SerializeField] GameObject Broken;
    [SerializeField] VisualEffect splashVFX;

    BoxCollider bc;
    // Start is called before the first frame update

    private void Awake()
    {
        Intact.SetActive(true);
        Broken.SetActive(false);

        bc = GetComponent<BoxCollider>();
    }

    private void Break()
    {
        Intact.SetActive(false);
        Broken.SetActive(true);
        splashVFX.enabled = true;
        splashVFX.Play();

        bc.enabled = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        Break();
    }
}