using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //global
    private GameManager game_manager;
    private InputManager input_manager;

    [Header("Movement Attributes")]
    [SerializeField] private float movement_speed = 20f;

    [Header("Movement - Linked Objects")]
    [SerializeField] private Rigidbody2D rigid_body = null;
    //[SerializeField] private GameObject player_model = null;

    //private variables
    private CameraScript main_camera_script;

    // Start is called before the first frame update

    private void Awake()
    {
        game_manager = Object.FindObjectOfType<GameManager>();
        input_manager = Object.FindObjectOfType<InputManager>();
        if (game_manager == null || input_manager == null) UnityEngine.SceneManagement.SceneManager.LoadScene("preload");
    }

    void Start()
    {
        main_camera_script = Camera.main.GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {

    /*  -this worked before, might be needed later
       input_manager.mouse_position.z = input_manager.mouse_position.z - main_camera.transform.position.z;
            real_mouse_position = main_camera.ScreenToWorldPoint(input_manager.mouse_position);
    */
    }

    private void FixedUpdate()
    {
        RotatePlayer();
        MovePlayer();
    }

    private void RotatePlayer()
    {
        //assuming that the rigid body has the correct coordinates - easier to do it like this, it is a 2D Vector
        Vector2 mouse_position_2D = main_camera_script.real_mouse_position;
        Vector2 look_direction = mouse_position_2D - rigid_body.position;
        float look_angle = Mathf.Atan2(look_direction.y, look_direction.x) * Mathf.Rad2Deg -90f;
        //rigid_body.transform.eulerAngles = new Vector3(0f, 0f, look_angle); -this works
        rigid_body.rotation = look_angle;
    }

    private void MovePlayer()
    {
        rigid_body.MovePosition(rigid_body.position + input_manager.movement_direction * movement_speed * Time.fixedDeltaTime);
        //rigid_body.AddForce(input_manager.movement_direction * movement_speed_add_force, ForceMode2D.Impulse); - clunky movement
    }


    //TO DO: AI to follow player, shoot range, detection range 
}
