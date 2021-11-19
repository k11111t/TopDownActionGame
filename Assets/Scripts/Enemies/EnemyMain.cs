using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMain : MonoBehaviour
{
    public enum EnemyState { IDLE, AGGRESIVE, DOWN, DEAD }

    public EnemyState enemy_state = EnemyState.IDLE;

    [SerializeField] private EnemyMovement movement_script = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemy_state)
        {
            case EnemyState.IDLE:
                movement_script.LookForPlayer();
                movement_script.random_movement = true;
                break;
            case EnemyState.AGGRESIVE:
                movement_script.FollowPlayer();
                movement_script.random_movement = false;
                break;
            case EnemyState.DOWN:
                //wait for a certain time to get up
                //check if the enemy was glory killed
                break;
            case EnemyState.DEAD:
                //disable everything
                break;
        }

        //check for player
        //use raycast, or boxcast, or conecast
    }

    

}