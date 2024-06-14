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

    float maxHealth;

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
        if (maxHealth != playerStats.health[StatsConst.CAP_INTENSITY])
        {
            maxHealth = playerStats.health[StatsConst.CAP_INTENSITY];
        }

        if (barFill.fillAmount != (playerStats.health[StatsConst.SELF_INTENSITY] / maxHealth))
        {
            barFill.fillAmount = playerStats.health[StatsConst.SELF_INTENSITY] / maxHealth;
        }
    }

    #endregion
    //========================


}
