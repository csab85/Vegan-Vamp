using System.Collections;
using System.Collections.Generic;
using TMPro;

using UnityEngine;

public class Speedometer : MonoBehaviour
{
    [SerializeField] Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = rb.velocity.magnitude.ToString();
    }
}
