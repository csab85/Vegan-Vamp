using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using UnityEngine;

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

    [SerializeField] float playerMaxDistance;
    [SerializeField] float playerDistance;
    bool loaded;

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
        if (playerDistance > playerMaxDistance && !loaded)
        {
            SceneManager.LoadSceneAsync("Village 1 - Teste");
            loaded = true;
        }

        else if (loaded)
        {
            SceneManager.UnloadSceneAsync("Village 1 - Teste");
            loaded = false;
        }
    }

    #endregion
    //========================


}
