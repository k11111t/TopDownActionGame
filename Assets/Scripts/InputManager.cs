using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //input manager might need to be reworked into new Input System

    //private variables
    public Vector2 movement_direction; 
    public Vector3 mouse_position;
    public bool mouse0_down;
    public bool mouse0_hold;
    public bool mouse1_down;
    public bool lshift_hold;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        // float value between -1 and 1
        movement_direction.x = Input.GetAxis("Horizontal");
        movement_direction.y = Input.GetAxis("Vertical");

        mouse_position = Input.mousePosition;
        mouse0_down = Input.GetButtonDown("Fire1");
        mouse0_hold = Input.GetButton("Fire1");
        mouse1_down = Input.GetButtonDown("Fire2");

        //might need some reworking for the controller
        lshift_hold = Input.GetKey(KeyCode.LeftShift);
    }
}
