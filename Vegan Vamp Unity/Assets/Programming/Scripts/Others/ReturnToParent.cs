using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToParent : MonoBehaviour
{
    [SerializeField] float secsToReturn;
    [SerializeField] GameObject parent;

    IEnumerator GoToParent()
    {
        yield return new WaitForSecondsRealtime(secsToReturn);
        transform.parent = parent.transform;
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        StartCoroutine(GoToParent());
    }
}
