using TMPro;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //component
    TextMeshProUGUI text;

    //scripts
    [SerializeField] Gun gun;

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



    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        text.text = $"{gun.capacity - gun.shotCounter}/{gun.capacity}";
    }

    #endregion
    //========================


}
