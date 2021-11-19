using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //Scripts
    private GameManager game_manager;
    private InputManager input_manager;
    private PlayerInteraction player_interaction;

    [Header("Player Fists Attributes")]
    [SerializeField] private float fist_attack_rate = 4f; //number of attacks per second
    [SerializeField] private Vector3 fist_offset = new Vector3(0, 0.5f, 0); //adjust these to change the range of the fists
    [SerializeField] private Vector2 fist_range = new Vector2(4, 3);
    CapsuleDirection2D capsule_direction = CapsuleDirection2D.Horizontal;
    float capsule_angle = 0;

    [Header("Player Combat - Prefabs to link")]
    [SerializeField] private GameObject weapon_slot = null;
    [SerializeField] private CapsuleCollider2D fist_collider = null;

    private float next_attack_time = 0f;
    private GameObject weapon_held = null;
    private WeaponScript weapon_held_script = null;

    private void Awake()
    {
        game_manager = Object.FindObjectOfType<GameManager>(); // these might be needed later
        input_manager = Object.FindObjectOfType<InputManager>();
        if (game_manager == null || input_manager == null) UnityEngine.SceneManagement.SceneManager.LoadScene("preload");

    }

    // Start is called before the first frame update
    void Start()
    {
        player_interaction = transform.GetComponent<PlayerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        // Attack
        //-- check for mouse hold for automatic guns - do later
        if (input_manager.mouse0_hold)
        {
            if (weapon_held == null)
            {
                UsePunch();
            }
            else
            {
                weapon_held_script.Attack();
            }
            return;
        }
        // Drop/Pick weapon
        if (input_manager.mouse1_down)
        {
            if(weapon_held == null)
            {
                PickUpWeapon();
            }
            else
            {
                DropWeapon();
                PickUpWeapon();
            }
            return;
        }
    }

    private void PickUpWeapon()
    {
        //will not pick up anything if there is no weapon in range
        List<GameObject> weapons_in_distance_list = player_interaction.GetObjectsInDistance();
        if(weapons_in_distance_list.Count > 0)
        {
            //easy way to do, might need rethinking
            GameObject weapon_to_equip = weapons_in_distance_list[0];
            SetUpWeapon(weapon_to_equip);

            //disable physics
            weapon_held.GetComponent<Rigidbody2D>().simulated = false;

            //set weapon holder
            weapon_held_script.SetWeaponHolder(gameObject);
        }
    }

    private void DropWeapon()
    {
        //enable physics
        weapon_held.GetComponent<Rigidbody2D>().simulated = true;
        weapon_held_script.ThrowAwayWeapon();

        //set weapon holder to null
        weapon_held_script.SetWeaponHolder(null);

        UnsetWeapon();
    }

    private void SetUpWeapon(GameObject weapon_to_equip)
    {
        //set parent and correct position and rotation
        weapon_to_equip.transform.SetParent(weapon_slot.transform);
        weapon_to_equip.transform.rotation = weapon_slot.transform.rotation;
        weapon_to_equip.transform.position = weapon_slot.transform.position;
        
        //assign variables
        weapon_held = weapon_to_equip;
        weapon_held_script = weapon_held.GetComponent<WeaponScript>();
    }

    private void UnsetWeapon()
    {
        //set them to null
        weapon_held.transform.SetParent(null);
        weapon_held_script = null;
        weapon_held = null;
    }

    private void UsePunch()
    {
        if (Time.time >= next_attack_time)
        {
            List<GameObject> enemies_hit = GetEnemiesInRange();
            //punch animation
            //detect hit
            next_attack_time = Time.time + 1 / fist_attack_rate;
        }
    }

    private List<GameObject> GetEnemiesInRange()
    {
        

        int layer_to_check = LayerMask.GetMask("Enemy");
        Collider2D[] enemies_hit = Physics2D.OverlapCapsuleAll(weapon_slot.transform.position + fist_offset, fist_range, capsule_direction, capsule_angle, layer_to_check);

        List<GameObject> enemies_obj_hit = new List<GameObject>();
        foreach(Collider2D enemy in enemies_hit)
        {
            enemies_obj_hit.Add(enemy.transform.gameObject);
        }

        return enemies_obj_hit;
    }
}
