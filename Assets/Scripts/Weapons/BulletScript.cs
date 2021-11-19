using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject weapon_holder = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //assuming weapon_holder is not null
        if (
            collision.attachedRigidbody != weapon_holder.GetComponent<Rigidbody2D>() //prevents "yourself"
            && !collision.transform.CompareTag("Bullet")
           )
        {
            //check window
            if (collision.CompareTag("Window"))
            {
                //destroy window
                collision.GetComponent<WindowScript>().ShatterWindow();
            }
            else
            {
                //print(collision.transform.gameObject.name);
                //add animation of bullet spark
                Destroy(gameObject, 0.001f);
            }            
        }

    }

    public void SetWeaponHolder(GameObject new_holder)
    {
        weapon_holder = new_holder;
    }
}
