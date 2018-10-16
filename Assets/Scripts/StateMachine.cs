using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public interface IState
{
    void Init();

    /// <summary>
    /// This method may not be called every frame. Use Update() for
    /// animations!
    /// </summary>
    void Tick();

    void End();
}

public abstract class AbstractState : MonoBehaviour, IState
{
    public BezierCurve2D[] paths;

    public abstract void Init();

    public abstract void Tick();

    public abstract void End();
}

class NullState : IState
{
    public void End()
    {
    }

    public void Init()
    {
    }

    public void Tick()
    {
    }
}

// A finite automaton may find itself 
// in any number of states at the time, thus
// we create wrapper to hold as many states as needed.
class AutomatonState
{
    List<IState> states = new List<IState>();

    public AutomatonState(params IState[] states)
    {
        for (int i = 0; i < states.Length; i++)
        {
            this.states.Add(states[i]);
        }
    }

    public void Init()
    {
        states.ForEach((s) => s.Init());
    }

    public void Tick()
    {
        states.ForEach((s) => s.Tick());
    }

    public void End()
    {
        states.ForEach((s) => s.End());
    }
}

public enum MachineActionState
{
    BUSY,
    READY
}

public class StateMachine : MonoBehaviour
{
    public static StateMachine Instance = null;

    List<Message>  activeMessages; 

    [SerializeField]
    Text txtCaption;

    MachineActionState actionState = MachineActionState.READY;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            // Do other stuff when first initializing
            Init();
        }
    }

    private AutomatonState currentState = null;
    private AutomatonState NULL_STATE = new AutomatonState(new NullState());

    private Queue<AutomatonState> states;

    private bool isRunning;

    public void StartStateMachine()
    {
        isRunning = true;
        currentState.Init();
    }

    public void Stop()
    {
        isRunning = false;
    }

    public AbstractState[] stateObjects;

    /// <summary>
    /// This is where we create our state machine. It's called once
    /// at the beginning.
    /// </summary>
    private void Init()
    {
        states = new Queue<AutomatonState>();
        activeMessages = new List<Message>();

        states.Enqueue(new AutomatonState(stateObjects[0]));
        states.Enqueue(new AutomatonState(stateObjects[1]));
        states.Enqueue(new AutomatonState(stateObjects[1],stateObjects[2]));
        // state gear to bucket, change doc image
        states.Enqueue(new AutomatonState(stateObjects[1].GetComponent<GearDocState>()));
        states.Enqueue(new AutomatonState(stateObjects[2].GetComponent<VaultMasterKeyState>()));

        currentState = states.Dequeue();
        actionState = MachineActionState.BUSY;

        //StartStateMachine();
    }

    void Update()
    {
        // TODO Always tick the current state, this may change
        // and tick every X instead of every frame
      
        if (isRunning)
        {

            if (actionState == MachineActionState.BUSY && taskCounter == 0)
            {
                actionState = MachineActionState.READY;
            }
            currentState.Tick();


            if (actionState == MachineActionState.READY)
            {
                SwitchState();
            } 

        }
    }

    private void SwitchState()
    {
        // We switch to another state
        if (states.Count > 0)
        {
            currentState = states.Dequeue();
        }
        else
        {
            // No more states
            currentState = NULL_STATE;
        }

        currentState.Init();
        actionState = MachineActionState.BUSY;
    }

    private int taskCounter = 0;

    public void AddTask()
    {
        ++taskCounter;

        Debug.Log("Task counter: " + taskCounter);
    }

    public void FinishTask()
    {
        --taskCounter;

        // If there are no mor tasks, we're ready
        if (taskCounter == 0)
            actionState = MachineActionState.READY;

        Debug.Log("Task counter: " + taskCounter);
    }

    public void SetCaption(string text)
    {
        txtCaption.text = text;
    }

    public void RegisterMsg (Message msg)
    {
        activeMessages.Add(msg);
    }

    public Message GetMessage(string msgName)
    {
        Message targetMsg = null; 
        foreach(Message m in activeMessages)
        {
            if (m.msgName.Equals(msgName)) targetMsg = m;
        }

        return targetMsg;
    }
}
