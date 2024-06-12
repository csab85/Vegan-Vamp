using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //component
    Image barFill;

    //scripts
    [SerializeField] StatsManager playerStats;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] public float maxHealth;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region



    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        barFill = GetComponent<Image>();
    }

    void Update()
    {
        barFill.fillAmount = playerStats.health[StatsConst.SELF_INTENSITY] / maxHealth;
    }

    #endregion
    //========================


}
