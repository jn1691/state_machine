using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BezierCurve2D))]
public class BezierCurve2DEditor : Editor
{
    private BezierCurve2D bCurve;
    private const int lineSteps = 10;

    Transform handleTransform;
    Quaternion handleRotation;

    private void OnSceneGUI()
    {
        bCurve = target as BezierCurve2D;

        bCurve.p0 = (Vector2)bCurve.startNode.position;
        bCurve.p2 = (Vector2)bCurve.endNode.position;

        handleTransform = bCurve.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;

        // points
        Vector2 p0 = ShowPoint(ref bCurve.p0);
        Vector2 p1 = ShowPoint(ref bCurve.p1);
        Vector2 p2 = ShowPoint(ref bCurve.p2);

        Handles.color = Color.gray;
        Handles.DrawLine(p0, p1);
        Handles.DrawLine(p1, p2);

        DrawBezier(p0, p1, p2);
    }

    private Vector2 ShowPoint(ref Vector2 p)
    {
        //Vector2 point = handleTransform.TransformPoint(p);
        Vector2 point = (p);
        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(bCurve, "Move point");
            EditorUtility.SetDirty(bCurve);
            //p = handleTransform.InverseTransformPoint(point);
            p = (point);
        }

        return point;
    }

    private void DrawBezier(Vector2 p0, Vector2 p1, Vector2 p2)
    {
        Handles.color = Color.white;
        Vector2 lineStart = bCurve.GetPoint(0f);

        // Handles.DrawLine(p0, p1) - draws a line between points
        // use GetPoint(t)
        int lineSteps = 100;
        for (int i = 0; i <= lineSteps; i++)
        {
            Vector3 lineEnd = bCurve.GetPoint(i / (float)lineSteps);
            Handles.DrawLine(lineStart, lineEnd);

            lineStart = lineEnd;
        }

    }
}
