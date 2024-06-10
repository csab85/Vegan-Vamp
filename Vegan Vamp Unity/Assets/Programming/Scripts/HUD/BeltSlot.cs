using UnityEngine;
using UnityEngine.EventSystems;

public class BeltSlot : MonoBehaviour, IPointerDownHandler
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
    StatsManager selfStats;
    StatsManager juiceStats;


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

    [ContextMenu ("SelectBottle")]
    public void SelectBottle()
    {   
        //activate selection
        GameObject selection = juiceIcon.transform.Find("Selection").gameObject;
        selection.SetActive(true);

        //update juice stats
        selfStats.PasteStats(juiceStats);
    }

    public void DeselectBottle()
    {
        //deactivate selection
        GameObject selection = juiceIcon.transform.Find("Selection").gameObject;
        selection.SetActive(false);
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

    public void OnPointerDown(PointerEventData eventData)
    {
        if (juiceIcon != null)
        {
            SelectBottle();
        }

        print("click");
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
