using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : AbstractState
{
    [SerializeField]
    Message msgDoc;
  

    public override void End()
    {
        throw new System.NotImplementedException();
    }

    public override void Init()
    {   
        Message m1 = Instantiate(msgDoc, transform.GetComponent<RectTransform>().anchoredPosition, Quaternion.identity);

        m1.transform.SetParent(transform.parent);

        m1.GetComponent<RectTransform>().anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;

        m1.Travel(paths[0]);
       // StartCoroutine(delayMessage(2, m2, paths[1]));

        StateMachine.Instance.AddTask();
        StateMachine.Instance.RegisterMsg(m1);

        StateMachine.Instance.SetCaption("start state: caption goes here...");
       // StateMachine.Instance.AddTask();
    }

    public override void Tick()
    {
    }

    private IEnumerator delayMessage (float delay, Message m, BezierCurve2D path) {
        yield return new WaitForSeconds(delay);
        m.Travel(path);

    }
}
