using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float activeTime = 2f;

    [Header("Mesh Related")]
    public float meshRefrashRate = 0.1f;
    public Transform positionToSpawn;

    private bool isTrailActive;
    private SkinnedMeshRenderer[] skinnedMeshRenderers;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !isTrailActive)
        {
            isTrailActive = true;
            StartCoroutine(ActiveTrail(activeTime));
        }
    }

    IEnumerator ActiveTrail (float timeActive)
    {
        while (activeTime > 0)
        {
            timeActive -= meshRefrashRate;

            if (skinnedMeshRenderers == null)
                skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

            for (int i = 0; i<skinnedMeshRenderers.Length; i++)
            {
                GameObject gObj = new GameObject();
                gObj.transform.SetPositionAndRotation(positionToSpawn.position, positionToSpawn.rotation);

                MeshRenderer mr = gObj.AddComponent<MeshRenderer>();
                MeshFilter mf =  gObj.AddComponent<MeshFilter>();

                Mesh mesh = new Mesh();
                skinnedMeshRenderers[i].BakeMesh(mesh);
                
                mf.mesh = mesh;
            }
            

            yield return new WaitForSeconds(meshRefrashRate);
        }

        isTrailActive = false;
    }
}
