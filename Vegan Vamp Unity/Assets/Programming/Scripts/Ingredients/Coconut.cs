using System.Collections;
using UnityEngine;

public class Coconut : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //components
    Rigidbody rb;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] Transform centerPoint;
    [SerializeField] float radius;
    [SerializeField] float translationSpeed;
    [SerializeField] float rotationSpeed;
    [SerializeField] float startDelay;

    float angle;
    bool spinning = false;
    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator WaitToSpin()
    {
        yield return new WaitForSeconds(startDelay);
        spinning = true;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        StartCoroutine(WaitToSpin());
    }

    void Update()
    {
        if (spinning)
        {
            // Increment the angle based on angular speed and time
            angle += translationSpeed * Time.deltaTime;

            // Calculate the new position
            float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
            float z = centerPoint.position.z + Mathf.Sin(angle) * radius;

            // Update the object's position
            rb.MovePosition(new Vector3(x, transform.position.y, z));

            rb.AddTorque(new Vector3(0, transform.position.y, 0));
        }
    }

    #endregion
    //========================


}
