using UnityEngine;
using UnityEngine.EventSystems;

public class BeltSlot : MonoBehaviour, IPointerDownHandler
{
    //IMPORTS
    //========================
    #region

    //game objects
    [SerializeField] GameObject baseJuice;
    GameObject juiceIcon;

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

    [ContextMenu ("SelectSelf")]
    public void SelectSelf()
    {   
        //activate selection
        GameObject selection = juiceIcon.transform.Find("Selection").gameObject;
        selection.SetActive(true);

        //update juice stats
        selfStats.PasteStats(juiceStats);
    }

    public void DeselectSelf()
    {
        //deactivate selection
        GameObject selection = juiceIcon.transform.Find("Selection").gameObject;
        selection.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag != "Ingredient")
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
        print("AAAAAAAAAAAAAAAAAA");
        SelectSelf();
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
    }

    #endregion
    //========================


}
