using UnityEngine;

public class moveTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetButton("Vertical"))
        {
            transform.position += transform.forward * 1 * Time.deltaTime;
        }
    }
}