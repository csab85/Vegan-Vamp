using UnityEngine;

public class SkyboxChanger : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] Material skybox;

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
            skybox = RenderSettings.skybox;
            transitioning = true;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.tag == "Player")
        {
            passedTime = 0;
            transitioning = false;
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        skybox = new Material(skybox);
        RenderSettings.skybox = skybox;
    }

    void Update()
    {
        if (transitioning)
        {
            float timePercentage = Mathf.Clamp01(passedTime / transitionTime);

            if (skybox.GetColor("_Top") != top)
            {
                timePercentage = Mathf.Clamp01(passedTime / transitionTime);

                Color newColor = Vector4.Lerp(skybox.GetColor("_Top"), top, timePercentage);

                skybox.SetColor("_Top", newColor);
            }

            if (skybox.GetColor("_Bottom") != bottom)
            {
                timePercentage = Mathf.Clamp01(passedTime / transitionTime);

                Color newColor = Vector4.Lerp(skybox.GetColor("_Bottom"), bottom, timePercentage);

                skybox.SetColor("_Bottom", newColor);
            }

            passedTime += Time.deltaTime;
            DynamicGI.UpdateEnvironment();

            if (timePercentage >= 1)
            {
                transitioning = false;
            }
        }
    }

    #endregion
    //========================


}
