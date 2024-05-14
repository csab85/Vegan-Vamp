using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[SelectionBase]
public class BreakBeattle : MonoBehaviour
{
    [SerializeField] GameObject Intact;
    [SerializeField] GameObject Broken;

    BoxCollider bc;
    // Start is called before the first frame update

    private void Awake()
    {
        Intact.SetActive(true);
        Broken.SetActive(false);

        bc = GetComponent<BoxCollider>();
    }

    private void OnMouseDown()
    {
        Break();
    }

    private void Break()
    {
        Intact.SetActive(false);
        Broken.SetActive(true);

        bc.enabled = false;
    }
}