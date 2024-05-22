using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] public static Vector3 customPosit;
    [SerializeField] public static bool customSpawn;

    private void Awake()
    {
        if (customSpawn)
        {
            transform.position = customPosit;
        }
    }
}
