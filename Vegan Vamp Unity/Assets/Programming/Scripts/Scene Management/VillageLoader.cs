using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

public class VillageLoader: MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject player;
    [SerializeField] GameObject[] deactivateArray;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] public float activationRadius;
    [SerializeField] float playerDistance;

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
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, transform.position);

        if (playerDistance < activationRadius && !deactivateArray[0].activeSelf)
        {
            foreach (GameObject obj in deactivateArray)
            {
                obj.SetActive(true);
            }
        }

        else if (playerDistance > activationRadius && deactivateArray[0].activeSelf)
        {
            foreach (GameObject obj in deactivateArray)
            {
                obj.SetActive(false);
            }
        }
    }

    #endregion
    //========================


}
