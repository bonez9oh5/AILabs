using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour {

    int currentState;
    int newState;
    
    [SerializeField]
    GameObject target;
    [SerializeField]
    Player player = null;
    [SerializeField]
    float attackDistance = 2.0f;
    [SerializeField]
    GameObject[] enemy;

    // Use this for initialization
    void Start () {

        player = FindObjectOfType<Player>();
        if (GameObject.FindGameObjectWithTag("Target") == true)
        {
            target = GameObject.FindGameObjectWithTag("Target");
        }
        else
        {
            Debug.Log("no target in scene.");
        }
        

        if (GameObject.FindGameObjectWithTag("Enemy") == true)
        {
            enemy = GameObject.FindGameObjectsWithTag("Enemy");
        }
        else
        {
            Debug.Log("no enemy in scene.");
        }
        
        currentState = 0;
        newState = 0;
		
	}
	
	// Update is called once per frame
	void Update () {
        
        if (player.GetDist() <= attackDistance)
        {
            if (player.BallInHand() == false)
            {
                newState = 1;
                StateTransition();
                player.SetState(currentState);
            }
            else
            {
                newState = 2;
                StateTransition();
                player.SetState(currentState);
            }
        }
        else if(player.GetDist() > attackDistance)
        {
            if (enemy[0] != null)
            {
                newState = 2;
                StateTransition();
                player.SetState(currentState);

            }
            else
            {
                newState = 0;
                StateTransition();
                player.SetState(currentState);
            }
        }
        
		
	}

    public int GetState()
    {
        return currentState;
    }

    public void SetState(int STATE)
    {
        newState = STATE;
    }

    void StateTransition()
    {
        currentState = newState;
    }
}
