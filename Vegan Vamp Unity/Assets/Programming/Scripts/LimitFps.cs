using UnityEngine;

public class LimitFps : MonoBehaviour
{
    [SerializeField]
    private float targetFrameRate = 60f;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = Mathf.RoundToInt(targetFrameRate);
    }
}