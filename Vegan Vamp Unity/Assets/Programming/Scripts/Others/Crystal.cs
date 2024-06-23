using Cinemachine;
using System.Collections;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] GameObject intact;
    [SerializeField] GameObject broke;
    [SerializeField] GameObject crystalSoul;

    //components
    [SerializeField] Transform[] islands;
    [SerializeField] CinemachineClearShot islandCam;
    [SerializeField] AudioClip audioCrystalBreak;
    AudioSource audioSource;

    //scripts
    [SerializeField] Movement playerMovement;
    StatsManager selfStats;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] float showTime;
    static int islandCounter = 0;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    IEnumerator ShowIsland()
    {
        islandCam.gameObject.SetActive(true);
        islandCam.Priority = 2;
        islandCam.LookAt = islands[islandCounter];
        islands[islandCounter].gameObject.GetComponent<Grow>().enabled = true;

        islandCounter++;

        //lock player
        playerMovement.moveSpeed = 0;

        yield return new WaitForSeconds(showTime);

        islandCam.Priority = 0;
        islandCam.gameObject.SetActive(false);
        playerMovement.moveSpeed = playerMovement.baseSpeed;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    private void Start()
    {
        selfStats = GetComponent<StatsManager>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (selfStats.dead)
        {
            if (intact.activeSelf)
            {
                intact.SetActive(false);
                broke.SetActive(true);

                //camera
                StartCoroutine(ShowIsland());

                //audio and vfx
                audioSource.clip = audioCrystalBreak;
                audioSource.Play();

                crystalSoul.SetActive(true);

                GetComponent<Collider>().enabled = false;
                GetComponent<Disappear>().enabled = true;
            }
        }
    }

    #endregion
    //========================


}
