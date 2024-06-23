using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FramerateControler : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] Toggle toggle;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI displayText;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    bool capping = true;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void SetFrameCap()
    {
        int frameCap = (int)slider.value;

        displayText.text = frameCap.ToString();

        Application.targetFrameRate = frameCap;
    }

    public void ToggleCapping()
    {
        if (toggle.isOn)
        {
            slider.interactable = true;
            SetFrameCap();
        }

        else
        {
            slider.interactable = false;
            Application.targetFrameRate = -1;
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    private void Start()
    {
        Application.targetFrameRate = 40;
    }

    #endregion
    //========================


}
