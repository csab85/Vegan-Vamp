using UnityEngine;

public class BeltSlot : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //game objects
    [SerializeField] GameObject baseJuice;
    public GameObject juiceIcon = null;

    //components
    CapsuleCollider2D selfCollider;

    //scripts
    [SerializeField] Hotbar hotbar;
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
                print("deselect");
                hotbar.DeselectAll();
                hotbar.selectedSlot = -1;
            }

            //run normally
            else
            {
                hotbar.DeselectAll();
                print("Select");
                //activate selection
                GameObject selection = juiceIcon.transform.Find("Selection").gameObject;
                selection.SetActive(true);

                //update juice stats
                selfStats.PasteStats(juiceStats);
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
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Juice")
        {   
            juiceIcon = collider.gameObject;
            selfStats.CopyStats(collider.gameObject.GetComponent<StatsManager>());
        }
    }

    void OnTriggerExit2D()
    {
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
        selfCollider = GetComponent<CapsuleCollider2D>();

        //get scripts
        selfStats = GetComponent<StatsManager>();
        juiceStats = baseJuice.GetComponent<StatsManager>();
    }

    #endregion
    //========================


}
