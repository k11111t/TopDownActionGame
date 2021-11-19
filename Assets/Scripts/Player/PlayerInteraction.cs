using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    //global
    private GameManager game_manager;
    private InputManager input_manager;


    //radius of a circle in which a player can interact with objects
    [SerializeField] private float detection_radius = 2f;

    private void Awake()
    {
        game_manager = Object.FindObjectOfType<GameManager>();
        input_manager = Object.FindObjectOfType<InputManager>();
        if (game_manager == null || input_manager == null) UnityEngine.SceneManagement.SceneManager.LoadScene("preload");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        //bullet detection
        if (collision.transform.CompareTag("Bullet")
            && collision.transform.GetComponent<BulletScript>().weapon_holder != gameObject)

        {
            print("weapon holder " + collision.transform.GetComponent<BulletScript>().weapon_holder.name);
            //play death animation
            //set Game State to Reset
            //print("Player hit by a bullet");
        }
    }

    public List<GameObject> GetObjectsInDistance()
    {
        /*//set up filter
        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(LayerMask.GetMask("Weapon"));*/

        //detect weapons in distance
        Collider2D[] collider2D_list = Physics2D.OverlapCircleAll(transform.position, detection_radius, LayerMask.GetMask("Weapon"));

        //convert to GameObject list
        List<GameObject> game_object_list = new List<GameObject>();
        foreach(Collider2D col in collider2D_list)
        {
            //check if the weapon is pickable, pick up
            if(col.transform.GetComponent<WeaponScript>().pickable)
                game_object_list.Add(col.gameObject);
        }

        return game_object_list;
    }
}
