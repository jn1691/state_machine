using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearState :  AbstractState
{
    public override void End()
    {
    }

 
    public override void Init()
    {

        Debug.Log("Init gear state");
    }

    [SerializeField]
    Vector3 rotVector;
    public override void Tick()
    {
        transform.Rotate(rotVector*Time.deltaTime);
    }
  
   
}
