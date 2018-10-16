using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField]
    private BezierCurve2D path;

    void Start()
    {
        if (path != null)
            transform.position = path.GetPoint(0);
    }

    void Update()
    {
        if (path != null)
            FollowPath();

        Tick();
    }

    float t = 0;

    private void FollowPath()
    {
        if(t < 1)
        {
            t += Time.deltaTime;
            transform.position = path.GetPoint(t);
        }
    }

    public void SetPath(BezierCurve2D path)
    {
        this.path = path;
    }

    public bool Done()
    {
        return t >= 1;
    }

    public virtual void Tick() { }

    public void Reset()
    {
        t = 0;
    }
}
