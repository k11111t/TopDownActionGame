using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject player;

    [Header("UI Panels")]
    public GameObject UI;

    enum State { MENU, INIT, INGAME, LOADLEVEL, FINISH }
    State current_state;

    // Start is called before the first frame update
    void Start()
    {
        BeginState(State.MENU);
    }

    // Update is called once per frame
    void Update()
    {
        switch (current_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.INGAME:
                break;
            case State.LOADLEVEL:
                break;
            case State.FINISH:
                break;
        }
    }

    private void BeginState(State new_state)
    {
        switch (new_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.INGAME:
                break;
            case State.LOADLEVEL:
                break;
            case State.FINISH:
                break;
        }
    }

    private void EndState()
    {
        switch (current_state)
        {
            case State.MENU:
                break;
            case State.INIT:
                break;
            case State.INGAME:
                break;
            case State.LOADLEVEL:
                break;
            case State.FINISH:
                break;
        }
    }

    private void SwitchState(State new_state)
    {
        EndState();
        BeginState(new_state);
        current_state = new_state;
    }
}
