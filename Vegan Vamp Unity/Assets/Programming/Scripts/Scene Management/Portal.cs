using UnityEngine.SceneManagement;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] string sceneName;
    [SerializeField] Vector3 customPosit;
    [SerializeField] bool customSpawn;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerManager.customSpawn = customSpawn;
            PlayerManager.customPosit = customPosit;
            SceneManager.LoadScene(sceneName);
        }
    }
}
