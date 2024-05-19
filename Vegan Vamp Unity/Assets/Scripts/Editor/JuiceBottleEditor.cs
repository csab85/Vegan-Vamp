using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(JuiceBottle))]
public class JuiceBottleEditor : Editor
{
    private void OnSceneGUI()
    {
        JuiceBottle juiceBottle = (JuiceBottle)target;

        Handles.color = Color.blue;
        Handles.DrawWireArc(juiceBottle.transform.position, Vector3.up, Vector3.forward, 360, juiceBottle.splashRange);
    }
}
