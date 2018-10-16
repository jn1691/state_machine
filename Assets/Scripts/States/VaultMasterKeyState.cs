using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultMasterKeyState : AbstractState
{
    public override void End()
    {
    }

    [SerializeField]
    Message masterDataKey;
    public override void Init()
    {
        Debug.Log("Init vault master key state");
        Message m1 = Instantiate(masterDataKey, transform.GetComponent<RectTransform>().anchoredPosition, Quaternion.identity);

        m1.transform.SetParent(transform.parent);

        m1.GetComponent<RectTransform>().anchoredPosition = transform.GetComponent<RectTransform>().anchoredPosition;

        m1.Travel(paths[0]);

        StateMachine.Instance.AddTask();
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
