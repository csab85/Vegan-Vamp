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

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] public float activationRadius;
    [SerializeField] float playerDistance;
    [SerializeField] string sceneName;
    bool loaded = false;

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

    private void Update()
    {
        playerDistance = Vector3.Distance(player.transform.position, transform.position);
        if (playerDistance < activationRadius && !loaded)
        {
            SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            Scene villageScene = SceneManager.GetSceneByName(sceneName);
        }

        else if (playerDistance > activationRadius && loaded)
        {
            SceneManager.UnloadSceneAsync(sceneName);
            loaded = false;
        }
    }

    #endregion
    //========================


}
