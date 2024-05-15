using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VillageLoader))]
public class VillageLoaderEditor : Editor
{
    private void OnSceneGUI()
    {
        VillageLoader loader = (VillageLoader)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(loader.transform.position, Vector3.up, Vector3.forward, 360, loader.activationRadius);
    }
}
