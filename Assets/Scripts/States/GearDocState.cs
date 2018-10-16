using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearDocState : AbstractState {
    public override void End()
    {
    }

    [SerializeField]
    Sprite imgDocLock;

    public override void Init()
    {
        Message docMsg = StateMachine.Instance.GetMessage("docMessage");

        if(docMsg != null) {
            docMsg.GetComponent<UnityEngine.UI.Image>().sprite = imgDocLock;
            docMsg.Reset();
            docMsg.Travel(paths[0]);
            StateMachine.Instance.AddTask();
        }


    }

    public override void Tick()
    {
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
