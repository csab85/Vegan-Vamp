using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RandomWalk))]
public class RandomWalkEditor : Editor
{
    private void OnSceneGUI()
    {
        RandomWalk randomWalk = (RandomWalk)target;

        Handles.color = Color.blue;
        Handles.DrawWireArc(randomWalk.transform.position, Vector3.up, Vector3.forward, 360, randomWalk.areaRadius);
    }
}
