using Cinemachine;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class SensitivityControler : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [Header ("Cameras")]
    [SerializeField] CinemachineFreeLook explorationCam;
    [SerializeField] CinemachineFreeLook combatCam;

    //components
    [Header("Display Texts")]
    [SerializeField] TextMeshProUGUI explorationXText;
    [SerializeField] TextMeshProUGUI explorationYText;
    [SerializeField] TextMeshProUGUI combatXText;
    [SerializeField] TextMeshProUGUI combatYText;


    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Exploration Camera Settings")]
    [SerializeField] float maxExplorationXSpeed;
    [SerializeField] float minExplorationXSpeed;
    [SerializeField] float maxExplorationYSpeed;
    [SerializeField] float minExplorationYSpeed;

    [Header("Combat Camera Settings")]
    [SerializeField] float maxCombatXSpeed;
    [SerializeField] float minCombatXSpeed;
    [SerializeField] float maxCombatYSpeed;
    [SerializeField] float minCombatYSpeed;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public void SetExplorationXSense(float intensity)
    {
        explorationCam.m_XAxis.m_MaxSpeed = Mathf.Lerp(minExplorationXSpeed, maxExplorationXSpeed, intensity);

        explorationXText.text = $"Eixo X: {(Mathf.Round(intensity * 100))}";
    }

    public void SetExplorationYSense(float intensity)
    {
        explorationCam.m_YAxis.m_MaxSpeed = Mathf.Lerp(minExplorationYSpeed, maxExplorationYSpeed, intensity);

        explorationYText.text = $"Eixo Y: {(Mathf.Round(intensity * 100))}";
    }

    public void SetCombatXSense(float intensity)
    {
        combatCam.m_XAxis.m_MaxSpeed = Mathf.Lerp(minCombatXSpeed, maxCombatXSpeed, intensity);

        combatXText.text = $"Eixo X: {(Mathf.Round(intensity * 100))}";
    }

    public void SetCombatYSense(float intensity)
    {
        combatCam.m_YAxis.m_MaxSpeed = Mathf.Lerp(minCombatYSpeed, maxCombatYSpeed, intensity);

        combatYText.text = $"Eixo Y: {(Mathf.Round(intensity * 100))}";
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    private void Start()
    {
        SetExplorationXSense(0.5f);
        SetExplorationYSense(0.5f);
        SetCombatXSense(0.5f);
        SetCombatYSense(0.5f);
    }

    #endregion
    //========================


}
