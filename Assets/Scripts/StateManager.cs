using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    GameManager gameManager;
    public State currentState;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    // Update is called once per frame
    void Update()
    {
        if(gameManager.gameIsActive == true)
        {
        RunStateMachine();
         
        }
    }
    private void RunStateMachine()
    {
        State nextState = currentState?.RunCurrentState();

        if (nextState != null)
        {
            //Switch to next state
            SwitchToTheNextState(nextState);

        }
    }
    public void SwitchToTheNextState(State nextState)
    {
        currentState = nextState;
    }
}
