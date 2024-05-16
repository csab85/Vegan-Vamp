using UnityEngine;
using UnityEditor;
using UnityEditor.AnimatedValues;

[CustomEditor (typeof(FieldOfView))]
public class FieldOfViewEditor : Editor
{
    //IMPORTS
    //========================
    #region



    #endregion
    //========================


    //STATS AND VALUES
    //========================
    #region



    #endregion
    //========================


    //FUNCTIONS
    //========================
    #region

    Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    #endregion
    //========================


    //RUNNING
    //========================
    #region

    void OnSceneGUI()
    {
        FieldOfView fov = (FieldOfView)target;

        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.visionRadius);

        Handles.color = Color.red;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.attackRadius);

        //till here creates a sphere showing the bigger radius

        Vector3 viewAngle1 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle2 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;

        if (fov.visionRadius > fov.attackRadius)
        {
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle1 * fov.visionRadius);
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle2 * fov.visionRadius);
        }

        else
        {
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle1 * fov.attackRadius);
            Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle2 * fov.attackRadius);
        }
        

        if (fov.isSeeingPlayer)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fov.transform.position, fov.player.transform.position);
        }
    }

    #endregion
    //========================


}
