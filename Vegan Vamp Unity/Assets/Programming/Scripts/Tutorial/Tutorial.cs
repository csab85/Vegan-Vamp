using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    //game objects
    [SerializeField]
    GameObject[] tutorialObjs;

    //components
    [Header ("Bottle Tutorial")]
    [SerializeField] TextMeshProUGUI bottleText;
    [SerializeField] Image bottleBg;

    [Header("Hotbar Tutorial")]
    [SerializeField] TextMeshProUGUI hotbarText;
    [SerializeField] Image hotbarBg;

    [Header("Throw Tutorial")]
    [SerializeField] TextMeshProUGUI throwText;
    [SerializeField] Image throwBg;

    TextMeshProUGUI fadeInText;
    Image fadeInBg;

    TextMeshProUGUI fadeOutText;
    Image fadeOutBg;


    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [Header ("Fade Settings")]
    [SerializeField] float fadeSpeed;

    bool fadingIn = false;
    bool fadingOut = false;

    public float tutorialSteps = -1;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    void FadeIn()
    {
        //text fade
        float newTextAlpha = Mathf.MoveTowards(fadeInText.color.a, 1, fadeSpeed);
        fadeInText.color = new Vector4(fadeInText.color.r, fadeInText.color.g, fadeInText.color.b, newTextAlpha);

        //background fadeIn
        float newBgAlpha = Mathf.MoveTowards(fadeInText.color.a, 1, fadeSpeed);
        fadeInBg.color = new Vector4(fadeInBg.color.r, fadeInBg.color.g, fadeInBg.color.b, newBgAlpha);

        if (newTextAlpha == 1 && newBgAlpha == 1)
        {
            fadingIn = false;
        }
    }

    void FadeOut()
    {
        //text fadeOut
        float newTextAlpha = Mathf.MoveTowards(fadeOutText.color.a, 0, fadeSpeed);
        fadeOutText.color = new Vector4(fadeOutText.color.r, fadeOutText.color.g, fadeOutText.color.b, newTextAlpha);

        //background fadeOut
        float newBgAlpha = Mathf.MoveTowards(fadeOutText.color.a, 0, fadeSpeed);
        fadeOutBg.color = new Vector4(fadeOutBg.color.r, fadeOutBg.color.g, fadeOutBg.color.b, newBgAlpha);

        if (newTextAlpha == 0 && newBgAlpha == 0)
        {
            fadingOut = false;

            //deactivate if at final stage
            if (tutorialSteps == 3)
            {
                foreach (GameObject tutorialObj in tutorialObjs)
                {
                    tutorialObj.SetActive(false);
                }

                gameObject.SetActive(false);
            }
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void Update()
    {
        if (fadingIn)
        {
            FadeIn();
        }

        if (fadingOut)
        {
            FadeOut();
        }

        switch (tutorialSteps)
        {
            //show drag bottle to belt
            case 0:

                if (fadeInText != bottleText)
                {
                    fadeInText = bottleText;
                    fadeInBg = bottleBg;

                    fadingIn = true;
                }

                break;

            case 1:

                //hide drag bottle tutorial
                if (fadeOutText != bottleText)
                {
                    fadeOutText = bottleText;
                    fadeOutBg = bottleBg;

                    fadingOut = true;
                }

                //show hotbar tutorial after other one faded out
                if (!fadingOut)
                {
                    fadeInText = hotbarText;
                    fadeInBg = hotbarBg;

                    fadingIn = true;
                }

                break;

            case 2:

                //hide hotbar tutorial
                if (fadeOutText != hotbarText)
                {
                    fadeOutText = hotbarText;
                    fadeOutBg = hotbarBg;

                    fadingOut = true;
                }

                //show throw bottle tutorial
                if (!fadingOut)
                {
                    fadeInText = throwText;
                    fadeInBg = throwBg;

                    fadingIn = true;
                }

            break;

            case 3:

                //hide hotbar tutorial
                if (fadeOutText != throwText)
                {
                    fadeOutText = throwText;
                    fadeOutBg = throwBg;

                    fadingOut = true;
                }

            break;
        }
    }

    #endregion
    //========================


}
