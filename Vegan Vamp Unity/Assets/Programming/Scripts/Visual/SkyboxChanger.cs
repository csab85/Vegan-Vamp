using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    Material skybox;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] Color top;
    [SerializeField] Color bottom;
    [SerializeField] float transitionTime;
    float passedTime;
    bool transitioning;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            transitioning = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            transitioning = false;
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Update()
    {
        if (skybox.GetColor("_Top") != top)
        {
            float timePercentage = Mathf.Clamp01(passedTime / transitionTime);

            top = Vector4.Lerp(skybox.GetColor("_Top"), top, timePercentage);

            skybox.SetColor("_Top", top);
        }

        if (skybox.GetColor("_Bottom") != bottom)
        {
            float timePercentage = Mathf.Clamp01(passedTime / transitionTime);

            top = Vector4.Lerp(skybox.GetColor("_Bottom"), bottom, timePercentage);

            skybox.SetColor("_Bottom", bottom);
        }
    }

    #endregion
    //========================


}
