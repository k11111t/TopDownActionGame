using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //global
    private GameManager game_manager;
    private InputManager input_manager;

    [Header("Camera Attributes")]
    [SerializeField] private Vector2 offset_2D; //make sure this is assigned
    [SerializeField] private float camera_speed = 0.75f;
    [SerializeField] private float camera_height = -24f;

    [Header("Camera Script - Linked Objects")]
    [SerializeField] private Camera main_camera = null;

    //private variables
    private Vector3 offset;
    private GameObject object_to_follow = null;
    private Vector3 player_location;
    private bool camera_locked = true;

    public Vector3 real_mouse_position = Vector3.zero;

    private void Awake()
    {
        game_manager = Object.FindObjectOfType<GameManager>();
        input_manager = Object.FindObjectOfType<InputManager>();
        if (game_manager == null || input_manager == null) UnityEngine.SceneManagement.SceneManager.LoadScene("preload");
    }

    void Start()
    {
        while(object_to_follow == null)
        {
            object_to_follow = GameObject.FindGameObjectWithTag("Player");
        }
        player_location = object_to_follow.transform.position;
        offset = new Vector3(offset_2D.x, offset_2D.y, camera_height);
    }

    private void Update()
    {
        FetchPlayerPosition();
        UpdateRealMousePosition();
        if (input_manager.lshift_hold)
        {
            //UpdateRealMousePosition();
            camera_locked = false;
        }
        else
        {
            camera_locked = true;
        }
    }

    private void FixedUpdate()
    {
        if (camera_locked)
        {
            MoveCameraWithOffset();
        }
        else
        {
            MoveCamera();
        }
        
    }

    private void FetchPlayerPosition()
    {
        player_location = object_to_follow.transform.position;
        player_location.z = camera_height;
    }

    private void UpdateRealMousePosition()
    {
        //get mouse position, set the z position - important! so the camera would not clip to Z=0
        input_manager.mouse_position.z = input_manager.mouse_position.z - transform.position.z;
        real_mouse_position = main_camera.ScreenToWorldPoint(input_manager.mouse_position);
        real_mouse_position.z = camera_height;
    }

    private void MoveCamera()
    {
        Vector3 camera_position = transform.position;
        //limit the max distance that the mouse can be on the screen! - DO LATER
        {
            transform.position = Vector3.Lerp(
                camera_position,
                (real_mouse_position - player_location)/2f + player_location,
                camera_speed);
        }
    }

    private void MoveCameraWithOffset()
    {
        Vector3 target_position = player_location + offset;
        transform.position = Vector3.Lerp(transform.position, target_position, camera_speed);
    }

}
