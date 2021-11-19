using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    //global
    private GameManager game_manager;

    [Header("Movement Attributes")]
    [SerializeField] private float movement_speed = 10f;

    [Header("Enemy Movement - Objects to link")]
    [SerializeField] private Rigidbody2D rigid_body = null;

    //private variables
    public bool random_movement = true;
    private bool isMoving = false;
    private Vector3 next_location;

    private float max_travel_distance = 3f;
    private float speed = 2f;
    private float sprint_speed = 7f;

    private GameObject object_to_follow;

    private void Awake()
    {
        game_manager = Object.FindObjectOfType<GameManager>();
        if (game_manager == null) UnityEngine.SceneManagement.SceneManager.LoadScene("preload");
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if(random_movement)
            MoveInRandomDirection();
    }

    //this is just for testing, real implementation will include random trajectory
/*    private void MoveOnTrajectory()
    {
        if ((transform.position - upcoming_location).magnitude <= 0.01f)
        {
            current_index++;
            if (current_index >= trajectory.Count)
                current_index = 0;
            upcoming_location = (Vector3) trajectory[current_index] + transform.position;
        }
        rigid_body.MovePosition(rigid_body.position + trajectory[current_index] * movement_speed * Time.fixedDeltaTime);
    }*/

    public void MoveInRandomDirection()
    {
        if (!isMoving)
        {
            float distance = Random.Range(-1, 1);
            int direction = Random.Range(0, 2);
            if(direction == 0) //0 is x direction
            {
                next_location = transform.position + Vector3.right * distance * max_travel_distance;
            }
            else //1 is y direction
            {
                next_location = transform.position + Vector3.up * distance * max_travel_distance;
            }
            isMoving = true;
        }

        rigid_body.MovePosition(transform.position + (next_location - transform.position) * speed * Time.fixedDeltaTime);
        isMoving = (transform.position - next_location).magnitude >= 0.01f || CheckForWall();
    }

    private bool CheckForWall()
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Wall"));
        List<RaycastHit2D> hit_objects_list = new List<RaycastHit2D>();
        Physics2D.Raycast(transform.position, (next_location - transform.position).normalized, filter, hit_objects_list, 0.5f);
        return hit_objects_list.Count > 0;
    }

    public void FollowPlayer()
    {
        rigid_body.MovePosition((object_to_follow.transform.position - transform.position).normalized * sprint_speed * Time.fixedDeltaTime);
    }

    public void LookForPlayer()
    {
        RaycastHit2D hit_object = Physics2D.Raycast(transform.position, Vector2.up);
        if (hit_object.collider != null)
        {
            GameObject hit_game_object = hit_object.collider.gameObject;
            if (hit_game_object.CompareTag("Player"))
            {
                print("player found!");
                object_to_follow = hit_game_object;
            }
        }
    }
}

