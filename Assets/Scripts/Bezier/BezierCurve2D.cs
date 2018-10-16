using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezier2D
{
    public static Vector2 GetPoint(Vector2 p0, Vector2 p1, Vector2 p2, float t)
    {
        float oneMinusT = 1 - t;
        return oneMinusT * oneMinusT * p0 + 2 * oneMinusT * t * p1 + t * t * p2;
    }

    public static Vector2 GetFirstDerivative(Vector2 p0, Vector2 p1, Vector2 p2, float t)
    {
        return Vector2.one;
    }
}

public class BezierCurve2D : MonoBehaviour
{
    public Transform startNode, endNode;

    public Vector2 p0 = new Vector2(0, 0), p1 = new Vector2(1, 1), p2 = new Vector2(2, 2);

    private void Start()
    {
        p0 = (Vector2) startNode.GetComponent<RectTransform>().position;
        p2 = (Vector2) endNode.position;
    }

    public Vector2 GetPoint(float t)
    {
        // make sure t is between 0, 1 in just in case
        t = Mathf.Clamp01(t);

        //return transform.TransformPoint(Bezier2D.GetPoint(p0, p1, p2, t));
        return (Bezier2D.GetPoint(p0, p1, p2, t));
    }

    public Vector2 GetFirstDerivative(float t)
    {
        t = Mathf.Clamp01(t);
        //return transform.TransformPoint(Bezier2D.GetFirstDerivative(p0, p1, p2, t));
        return (Bezier2D.GetFirstDerivative(p0, p1, p2, t));
    }
}
