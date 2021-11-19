using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    //global
    private GameManager game_manager;
    private InputManager input_manager;


    private void Awake()
    {
        game_manager = Object.FindObjectOfType<GameManager>();
        input_manager = Object.FindObjectOfType<InputManager>();
        if (game_manager == null || input_manager == null) UnityEngine.SceneManagement.SceneManager.LoadScene("preload");
    }

    //yet to decide whether the bullet will have solid collider
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //bullet detection
        if (collision.transform.CompareTag("Bullet")
            && collision.transform.GetComponent<BulletScript>().weapon_holder != gameObject)
        {
            //play death animation
            //disable all on this object
            //print("Hit by a Bullet");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Weapon") 
            && collision.transform.GetComponent<WeaponScript>().is_flying)
        {
            //play knockdown animation
            //print("Hit by a Weapon");

        }
    }

    private void SetKnockDown()
    {

    }


}
