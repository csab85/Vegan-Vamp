using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RandomFlight))]
public class RandomFlightEditor : Editor
{
    private void OnSceneGUI()
    {
        RandomFlight randomFlight = (RandomFlight)target;

        Handles.color = Color.blue;
        Handles.DrawWireArc(randomFlight.transform.position + randomFlight.offset, Vector3.up, Vector3.forward, 360, randomFlight.areaRadius);
        Handles.DrawWireArc(randomFlight.transform.position + randomFlight.offset, Vector3.forward, Vector3.up, 360, randomFlight.areaHeight);
    }
}
