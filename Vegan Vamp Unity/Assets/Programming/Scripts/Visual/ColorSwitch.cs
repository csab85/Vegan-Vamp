using System.Collections.Generic;
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
    Material liquid;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region
    
    int colorIndex = 0;

    //colors
    List<Color> colors = new List<Color>();
    Color selfColor;

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

    void ChangeColor()
    {
        if (selfColor != colors[colorIndex])
        {
            selfColor = Vector4.MoveTowards(selfColor, colors[colorIndex], Time.deltaTime);
        }

        else
        {
            colorIndex = (colorIndex + 1) % colors.Count;
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    private void Start()
    {
        //get scripts
        selfStats = GetComponent<StatsManager>();

        //get colors from stats
        foreach (Color color in selfStats.colors)
        {
            colors.Add(color);
        }

        selfColor = colors[0];        

        //get material if 3d
        if (dimension == Dimension.obj3D)
        {
            liquid = liquidObj.GetComponent<Renderer>().material;
        }
    }

    private void Update()
    {
        //update if colors change
        if (colors.Count != selfStats.colors.Count)
        {
            colors = new List<Color>();

            foreach (Color color in selfStats.colors)
            {
                colors.Add(color);
            }
        }

        else
        {
            for (int i = 0; i < colors.Count; i++)
            {
                if (colors[i] != selfStats.colors[i])
                {
                    colors[i] = selfStats.colors[i];
                }
            }
        }

        //get color value
        ChangeColor();


        //update image
        if (dimension == Dimension.obj2D)
        {
            icon.color = selfColor;
        }

        //update model
        if (dimension == Dimension.obj3D)
        {
            liquid.SetColor("_Fresnel_Color2", selfColor);
            liquid.SetColor("_Surface_Color", selfColor);
        }
    }

    #endregion
    //========================


}
