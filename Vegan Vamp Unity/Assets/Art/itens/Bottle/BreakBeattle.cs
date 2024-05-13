using System.Collections;
using System.Collections.Generic;
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
    }