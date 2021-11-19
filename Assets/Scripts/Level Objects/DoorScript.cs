using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{    
    [Header("Door Attributes")]
    [SerializeField] private float rotation_speed = 2f;

    [Header("Linked Objects")]
    //[SerializeField] private GameObject door_model = null;
    //[SerializeField] private BoxCollider2D trigger_collider = null;
    [SerializeField] private Rigidbody2D rigid_body = null;

    void Start()
    {
        // sets the rotation point
        rigid_body.centerOfMass = new Vector2(0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string collision_object_tag = collision.tag;
        switch (collision_object_tag)
        {
            case "Player":
                //print("player hit door");
                //get player's speed and use it in torgue - may be included
                Vector2 player_speed = collision.transform.GetComponent<Rigidbody2D>().velocity;
                rigid_body.AddTorque(rotation_speed * player_speed.magnitude, ForceMode2D.Impulse);
                //adjust rotation speed accordingly
                rigid_body.AddTorque(rotation_speed * 1.5f, ForceMode2D.Impulse);
                break;
            case "Weapon":
                //print("weapon hit the door");
                collision.transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                break;
            case "Enemy":
                break;
            default:
                //print("unsuccessful");
                break;
        }
    }


    /*    private void OnDrawGizmos() - only for debugging
        {
            Gizmos.DrawWireSphere(rigid_body.centerOfMass + new Vector2(transform.position.x, transform.position.y), 5.12f);
        }*/

}
