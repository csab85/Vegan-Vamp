using UnityEngine;

public class BossArea : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] AudioClip audioBossTheme;
    [SerializeField] BasicBehaviour bossBehaviour;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region



    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<AudioSource>().clip = audioBossTheme;
            other.gameObject.GetComponent<AudioSource>().Play();

            //boss
            bossBehaviour.baseVisionAngle = 360;
            bossBehaviour.gameObject.GetComponent<FieldOfView>().angle = 360;
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region



    #endregion
    //========================


}
