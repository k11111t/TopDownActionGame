using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : WeaponScript
{
    [Header("Melee Weapon Attributes")]
    [SerializeField] private int attack_speed = 3; //number of attacks per second
    
    private float next_attack_time = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Attack()
    {
        if(Time.time >= next_attack_time)
        {
            //animation
            //attack
            next_attack_time = Time.time + 1f / attack_speed;
        }
    }
}
