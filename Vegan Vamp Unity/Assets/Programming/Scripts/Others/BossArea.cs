using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BossArea : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] AudioClip audioBossTheme;
    [SerializeField] AudioClip audioOverworldTheme;
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

    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<AudioSource>().clip = audioOverworldTheme;
        other.gameObject.GetComponent<AudioSource>().Play();
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region



    #endregion
    //========================


}
