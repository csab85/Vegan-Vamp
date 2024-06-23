using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundControler : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //components
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI masterDisplayText;
    [SerializeField] TextMeshProUGUI sfxDisplayText;
    [SerializeField] TextMeshProUGUI musicDisplayText;

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

    public void SetMasterVolume()
    {
        float volume = slider.value;
        mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);

        string displayValue = Mathf.Round(volume * 100).ToString();

        masterDisplayText.text = $"Geral ({displayValue})";
    }

    public void SetSFXVolume()
    {
        float volume = slider.value;
        mixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);

        string displayValue = Mathf.Round(volume * 100).ToString();

        sfxDisplayText.text = $"Efeitos ({displayValue})";
    }

    public void SetMusicVolume()
    {
        float volume = slider.value;
        mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);

        string displayValue = Mathf.Round(volume * 100).ToString();

        musicDisplayText.text = $"Música ({displayValue})";
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    private void Start()
    {
        SetMasterVolume();
        SetSFXVolume();
        SetMusicVolume();
    }

    #endregion
    //========================


}
