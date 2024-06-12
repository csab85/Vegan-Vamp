using UnityEngine;
using UnityEngine.UI;

public class ColorSwitch: MonoBehaviour
{
    //IMPORTS
    //========================
    #region
       

    //scripts
    StatsManager selfStats;

    [SerializeField] GameObject liquidObj;

    //images
    [SerializeField] Image icon;

    //materials
    [SerializeField] Material liquid;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    readonly float colorIndex;

    //colors
    [SerializeField] Color color1;
    [SerializeField] Color color2;

    //object type
    enum Dimension
    {
        obj2D,
        obj3D
    }

    [SerializeField] Dimension dimension;

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

    private void Start()
    {
        liquid = liquidObj.GetComponent<Renderer>().material;
        Renderer renderer = liquidObj.GetComponent<Renderer>();
        liquid = new Material(liquid);
        renderer.material = liquid;
        
        

    }

    private void Update()
    {
        if (dimension == Dimension.obj2D)
        {
            icon.color = color1;
        }

        
            
            liquid.SetColor("_Fresnel_Color2", color1);
            liquid.SetColor("_Surface_Color", color2);
           
            
        
    }

    #endregion
    //========================


}
