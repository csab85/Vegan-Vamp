using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResolutionControl  : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] TMP_Dropdown resolutionDropdown;

    Resolution[] resolutions;
    List<Resolution> filteredResolutions;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    float currentRefreshRate;
    int currentResolutionIndex;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];

        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    private void Start()
    {
        //set vars
        resolutions = Screen.resolutions;

        filteredResolutions = new List<Resolution>();

        currentRefreshRate = (float)Screen.currentResolution.refreshRateRatio.value;

        resolutionDropdown.ClearOptions();

        //get refresh rates
        for (int i = 0; i < resolutions.Length; i++)
        {
            if ((float)resolutions[i].refreshRateRatio.value == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        //put resolutions on menu dropdown
        List<string> options = new List<string>();

        for (int i =0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = $"{filteredResolutions[i].width} x {filteredResolutions[i].height} {Mathf.RoundToInt((float)filteredResolutions[i].refreshRateRatio.value)} Hz";
            options.Add(resolutionOption);

            //set right resolution
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    #endregion
    //========================


}
