using System;
using System.Collections;
using System.Data;
using TMPro;
using UnityEngine;

public class LoadText : MonoBehaviour
{
    //IMPORTS
    //========================
    #region

    [SerializeField] TextMeshProUGUI[] textMeshes;

    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region

    [SerializeField] string[] texts;

    public int printedTexts = 0;
    public int deletedTexts = 0;
    public bool typing = false;
    public bool waiting = false;
    public bool waited = false;

    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    public IEnumerator PrintText(int textIndex, float printTime, bool wait = false)
    {   
        TextMeshProUGUI textMesh = textMeshes[textIndex];
        string text = texts[textIndex];
        int textLength = text.Length;

        //set if it is waiting or typing
        if (wait)
        {
            waiting = true;
        }

        else
        {
            typing = true;
        }

        //print text
        foreach (char letter in text)
        {
            textMesh.text += letter;
            yield return new WaitForSeconds(printTime);
        }

        if (wait)
        {
            waiting = false;
            waited = true;
        }

        else
        {
            typing = false;
            waited = false;
            printedTexts ++;   
        }
    }

    public IEnumerator DeleteText(int textIndex, float deleteTime, bool wait = false)
    {
        TextMeshProUGUI textMesh = textMeshes[textIndex];
        int textLength = textMesh.text.Length;

        //set if it is waiting or typing
        if (wait)
        {
            waiting = true;
        }

        else
        {
            typing = true;
        }

        //delete text
        for (int i = 1; i <= textLength; i++)
        {
            textMesh.text = textMesh.text.Remove(textLength - i);
            yield return new WaitForSeconds(deleteTime);
        }

        if (wait)
        {
            waiting = false;
            waited = true;
        }

        else
        {
            typing = false;
            waited = false;
            deletedTexts ++;   
        }
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region



    #endregion
    //========================


}
