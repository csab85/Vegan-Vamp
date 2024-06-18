using UnityEngine;

public class BeltSlot : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //game objects
    [SerializeField] GameObject player;
    [SerializeField] GameObject baseJuice;
    public GameObject juiceIcon = null;

    //components
    Animator animator;

    //scripts
    [SerializeField] Hotbar hotbar;
    [SerializeField] Tutorial tutorial;
    StatsManager selfStats;
    StatsManager juiceStats;


    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] int slotIndex;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    [ContextMenu ("SelectBottle")]
    public void SelectBottle()
    {
        if (juiceIcon != null)
        {
            //deselected if already slected
            if (juiceIcon.transform.Find("Selection").gameObject.activeSelf)
            {
                hotbar.DeselectAll();
                hotbar.selectedSlot = -1;

                //manage juice
                baseJuice.GetComponent<JuiceBottle>().DeactivateBottle();
                animator.SetLayerWeight(AnimationConsts.BOTTLE_LAYER, 0);
            }

            //run normally
            else
            {
                hotbar.DeselectAll();

                //activate selection
                GameObject selection = juiceIcon.transform.Find("Selection").gameObject;
                selection.SetActive(true);

                //update base juice stats
                if (!baseJuice.transform.Find("Intact").gameObject.activeSelf)
                {
                    baseJuice.GetComponent<JuiceBottle>().ActivateBottle();
                    animator.SetLayerWeight(AnimationConsts.BOTTLE_LAYER, 1);
                }

                selfStats.PasteStats(juiceStats);

                //tutorial
                tutorial.tutorialSteps = 2;
            }
        }
    }

    public void DeselectBottle()
    {
        if (juiceIcon != null)
        {
            //deactivate selection
            GameObject selection = juiceIcon.transform.Find("Selection").gameObject;
            selection.SetActive(false);

            //deactivate base juice
            baseJuice.GetComponent<JuiceBottle>().DeactivateBottle();
        }
    }

    public void DeleteBottle()
    {
        if (juiceIcon != null)
        {
            if (juiceIcon.transform.Find("Selection").gameObject.activeSelf)
            {
                Destroy(juiceIcon);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Juice")
        {   
            juiceIcon = collider.gameObject;
            selfStats.CopyStats(collider.gameObject.GetComponent<StatsManager>());

            //tutorial
            tutorial.tutorialSteps = 1;
        }
    }

    void OnTriggerExit2D()
    {
        DeselectBottle();
        juiceIcon = null;
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Start()
    {
        //get components
        animator = player.GetComponent<Animator>();

        //get scripts
        selfStats = GetComponent<StatsManager>();
        juiceStats = baseJuice.GetComponent<StatsManager>();
    }

    #endregion
    //========================


}
