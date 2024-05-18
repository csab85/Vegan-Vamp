using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Interactions))]
public class InteractionsEditor : Editor
{
    private void OnSceneGUI()
    {
        Interactions interactions = (Interactions)target;

        Handles.color = Color.blue;
        Handles.DrawWireArc(interactions.transform.position, Vector3.up, Vector3.forward, 360, interactions.interactionRange);
        Handles.DrawWireArc(interactions.transform.position, Vector3.forward, Vector3.up, 360, interactions.interactionRange);
    }
}
