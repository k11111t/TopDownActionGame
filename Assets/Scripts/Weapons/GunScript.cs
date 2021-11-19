using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : WeaponScript
{
    // Start is called before the first frame update

    [Header("Gun Attributes")]
    [SerializeField] private int max_magazine_size = 100;
    [SerializeField] private int multi_shot = 1;
    [SerializeField] private float fire_power = 3f;
    [SerializeField] private float accuracy = 0.9f; //should be value between 0 - 1
    [SerializeField] private int attack_speed = 8;

    [Header("Bullet Attributes")]
    [SerializeField] private float bullet_life_span = 5f;
    [SerializeField] private float angle_multiplier = 1f;

    [Header("Gun Script - Linked Objects")]
    [SerializeField] private GameObject bullet_prefab = null;
    [SerializeField] private Transform fire_point = null;

    //private variables
    private int current_magazine_size; // might need to be made public later for UI
    private float next_attack_time = 0f;

    void Start()
    {
        current_magazine_size = max_magazine_size;
    }

    public override void Attack()
    {
        if(Time.time > next_attack_time)
        {
            if (current_magazine_size > 0)
            {
                ShootBullets();
            }
            current_magazine_size--;

            next_attack_time = Time.time + 1f / attack_speed;
        }
        
    }

    private void ShootBullets()
    {
        for (int i = 0; i < multi_shot; i++)
        {
            GameObject bullet = Instantiate(bullet_prefab, fire_point.position, transform.rotation);

            //calculate inaccuracy offset
            float bullet_position_x = Random.Range(-(1 - accuracy), (1 - accuracy));
            float bullet_position_y = Random.Range(-(1 - accuracy), (1 - accuracy));
            Vector3 new_bullet_direction = transform.up + new Vector3(bullet_position_x, bullet_position_y) * angle_multiplier;

            //calculate new angle of the bullet
            float new_angle = Vector3.Angle(transform.up, new_bullet_direction);
            Quaternion new_rotation = Quaternion.AngleAxis(new_angle, transform.up) * transform.rotation;
            bullet.transform.rotation = new_rotation;

            //calculate fire power
            float new_bullet_speed = fire_power * Random.Range(0.9f, 1.0f);

            //shoot the bullet
            bullet.GetComponent<Rigidbody2D>().AddForce(
                (new_bullet_direction.normalized) * new_bullet_speed,
                ForceMode2D.Impulse
                );

            //need to tell the bullet who shot it
            //enemies cannot hit each other - DO LATER
            bullet.GetComponent<BulletScript>().SetWeaponHolder(weapon_holder);
            Destroy(bullet, bullet_life_span);
        }
    }

}
